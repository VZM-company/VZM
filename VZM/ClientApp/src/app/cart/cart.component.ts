import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { AlertDialogComponent } from '../dialogs/alert-dialog/alert-dialog.component';
import { UserService } from '../services/user.service';

@Component({
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  items = [];

  total: number = 0;

  loading = false

  constructor(
    private api: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private userService: UserService,
    private dialog: MatDialog,
    private router: Router,
    private sanitizer: DomSanitizer,
  ) {
    this.total = 0;
    this.loading = true;
    this.api.get(this.baseUrl + 'api/cart').subscribe(result => {
      let items: any[] = [];
      for (let item of result as []) {
        let days = item['left'] as number;

        let daysString = days == 0 ? '' : `${days} day${days > 1 ? 's' : ''} left`;
        items.push({
          actualPrice: item['actualPrice'],
          discount: item['discount'],
          left: daysString,
          name: item['name'],
          price: item['price'],
          imageUrl: this.sanitizer.bypassSecurityTrustResourceUrl(item['imageUrl']) as string,
          productId: item['productId'],
        });
        this.items = items;
        this.total = this.items.reduce((prev, next) => { next.price = prev.price + next.price; return next }).price;
      }
      this.loading = false;
    }, error => {
      this.loading = false;
      console.error(error)
    })
  }

  purchase() {
    this.loading = true
    this.api.post(this.baseUrl + 'api/cart/purchase', {}).subscribe(result => {
      this.dialog.open(AlertDialogComponent, { data: { title: "Cart action", description: "Purchased!" } });
    }, error => {
      this.dialog.open(AlertDialogComponent, { data: { title: "Cart action", description: "Error!" } });
    this.loading = true
      console.error(error)
    }, () => {
        this.loading = false;
    })
  }

  ngOnInit(): void {
      
  }
}
