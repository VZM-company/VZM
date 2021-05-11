import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

export interface findProductModel {
  startPrice: number | string,
  endPrice: number | string,
  title: string,
  category: string | null,
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  items = [];

  items_other = this.items;

  constructor(
    private userService: UserService,
    private api: HttpClient,
    private router: Router,
    private sanitizer: DomSanitizer,
    @Inject('BASE_URL') private baseUrl: string,
  ) {
    //if (this.userService.isAuthenticated) {
    //  let user = this.userService.getUser();
    //  this.api.post(this.baseUrl + "api/user/login", { "UserName": user['userName'], "Password": user['passwordHash'] }).subscribe(result => {
    //  }, error => console.error(error));
    //}

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

  toDetail(productId) {
    if (productId) {
      this.router.navigate(["/app-detail", productId]);
    }
  }

  ngOnInit(): void {
      
  }
}
