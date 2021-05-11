import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertDialogComponent } from '../dialogs/alert-dialog/alert-dialog.component';
import { ConfirmDialogComponent } from '../dialogs/confirm-dialog/confirm-dialog.component';
import { UserService, productModel } from '../services/user.service';

@Component({
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  items = [];

  profile = {
    name: "Lorem",
    description: "Lorem ipsum hey...",

  }
  loading: boolean;

  apiUrl: string;
  products: productModel[];
  isSeller: boolean;

  constructor(
    private userService: UserService,
    private router: Router,
    @Inject('BASE_URL') private baseUrl: string,
    private api: HttpClient,
    private sanitizer: DomSanitizer,
    private dialog: MatDialog,
  ) {
    this.api = api;
    this.baseUrl = baseUrl;
    this.userService = userService;
    this.apiUrl = this.baseUrl + 'api/user';
    this.sanitizer = sanitizer;
    this.router = router;
    this.dialog = dialog;
  }

  toDetail(ProductId) {
    if (this.userService.isSeller) {
      this.router.navigate(['/profile/', ProductId]);
    } else {
      this.router.navigate(['/app-detail/', ProductId]);
    }
  }

  delete(ProductId) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, { data: {}});

    dialogRef.afterClosed().subscribe(result => {
      if (result == true) {
        this.api.delete(this.baseUrl + 'api/products/' + ProductId).subscribe(res => {
          let ref = this.dialog.open(AlertDialogComponent, { data: { title: "Product action", description: "Product was deleted successfully!" } });
          ref.afterClosed().subscribe(res => {
            this.router.navigate([".."]);
          })
        }, error => console.error(error))
      }
    });
  }

  ngOnInit(): void {
    if (this.apiUrl != null) {
      this.loading = true;
      this.api.post(this.apiUrl + '/products', { "userId": this.userService.getUser()['userId'] }).subscribe(result => {
        let items: any[] = [];
        //console.log("qwerqwer", result);
        for (let item of result as []) {
          let days = item['left'] as number;

          let daysString = days == 0 ? '' : `${days} day${days > 1 ? 's' : ''} left`;
          items.push({
            //actualPrice: item['price'],
            //discount: item['price'],
            //left: item['price'],
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
      }, error => console.error(error), () => { this.loading = false; });
    }
  }
}
