import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  items = [];

  total: number = 0;

  constructor(
    private api: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private userService: UserService,
    private dialog: MatDialog,
    private router: Router,
    private sanitizer: DomSanitizer,
  ) {
    this.total = this.items.reduce((prev, next) => { next.price = prev.price + next.price; return next }).price;
    this.api.get(this.baseUrl + 'api/products/top').subscribe(result => {
      console.log("result from api/products/find is ", result)
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
      }
    }, error => console.error(error))
  }

  pay() {

  }

  ngOnInit(): void {
      
  }
}
