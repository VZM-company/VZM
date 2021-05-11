import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit, Sanitizer } from '@angular/core';
import { MatDialog } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertDialogComponent } from '../dialogs/alert-dialog/alert-dialog.component';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './app-detail.html',
  styleUrls: ['./app-detail.css']
})
export class AppDetailComponent implements OnInit {
  item: {
    title: string,
    description: string,
    price: number,
    discount: number,
    imageUrl: string,
    actualPrice: number,
    left: string,
    productId: number,
  };

  loading = false;

  constructor(
    private api: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private dialog: MatDialog,
    private router: Router,
    private sanitizer: DomSanitizer,
  ) {
    this.loading = true;
    this.activatedRoute.params.subscribe(params => {
      this.api.get(this.baseUrl + 'api/products/' + params['id']).subscribe(item => {
        let days = item['left'] as number;
        let daysString = days == 0 ? '' : `${days} day${days > 1 ? 's' : ''} left`;

        this.item = {
          actualPrice: item['actualPrice'],
          discount: item['discount'],
          left: daysString,
          title: item['title'],
          price: item['price'],
          description: item['description'],
          imageUrl: this.sanitizer.bypassSecurityTrustResourceUrl(item['imageUrl']) as string,
          productId: item['productId'],
        }
      }, error => console.error(error), () => { this.loading = false})
    })
  }

  addToCart() {
    this.loading = true;
    this.api.post(this.baseUrl + 'api/cart/add', { ProductId: this.item.productId }).subscribe(res => {
      let ref = this.dialog.open(AlertDialogComponent, { data: { title: "Cart action", description: "Added to cart successfully!" } });
      ref.afterClosed().subscribe(() => {
        this.router.navigate(["/cart"]);
      })
    }, error => {
        console.error(error)
        this.dialog.open(AlertDialogComponent, { data: { title: "Cart action", description: "Error happened while adding to cart!" } });
        this.loading = false
    }, () => { this.loading = false })
  }

  toAuthPage() {
    let ref = this.dialog.open(AlertDialogComponent, { data: { title: "Cart action", description: "You need to be authenticated first!" } })
    ref.afterClosed().subscribe(() => {
      this.router.navigate(['/auth']);
    })
  }


  ngOnInit(): void {
      
  }
}
