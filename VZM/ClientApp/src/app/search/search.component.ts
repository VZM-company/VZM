import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  items = [];
  
  searchForm = new FormGroup({
    startPrice: new FormControl(""),
    endPrice: new FormControl(""),
    category: new FormControl(""),
    title: new FormControl(""),
  });

  categories = [{
    name: "шутан",
    value: "shooter"
  }, {
      name: "rpg",
      value: "rpg"
    },];

  loading = false;

  constructor(
    private userService: UserService,
    private api: HttpClient,
    private sanitizer: DomSanitizer,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router,
  ) {
    this.findByUrl(this.baseUrl + 'api/products/top');
  }

  timeout: any = null;

  onKeySearch(event: any) {
    clearTimeout(this.timeout);
    var $this = this;
    this.timeout = setTimeout(function () {
      if (event.keyCode != 13) {
        $this.search();
      }
    }, 1000);
  }

  search() {
    let queryString = new URLSearchParams({
      startPrice: this.searchForm.get("startPrice").value ? this.searchForm.get("startPrice").value : 0,
      category: this.searchForm.get("category").value ? this.searchForm.get("category").value : undefined,
      endPrice: this.searchForm.get("endPrice").value ? this.searchForm.get("endPrice").value : 0,
      title: this.searchForm.get("title").value ? this.searchForm.get("title").value : '',
    }).toString();
    this.findByUrl(this.baseUrl + 'api/products/find?' + queryString);
    //this.api.get(this.baseUrl + 'api/products/find?' + queryString).subscribe(result => {
    //  let items: any[] = [];
    //  for (let item of result as []) {
    //    let days = item['left'] as number;

    //    let daysString = days == 0 ? '' : `${days} day${days > 1 ? 's' : ''} left`;
    //    items.push({
    //      actualPrice: item['actualPrice'],
    //      discount: item['discount'],
    //      left: daysString,
    //      name: item['name'],
    //      price: item['price'],
    //      imageUrl: this.sanitizer.bypassSecurityTrustResourceUrl(item['imageUrl']) as string,
    //      productId: item['productId'],
    //    });
    //    this.items = items;
    //  }
    //}, error => console.error(error))
  }

  findByUrl(url) {
    this.loading = true;
    this.items = [];
    this.api.get(url).subscribe(result => {
      let items = [];
      for (let item of result as []) {
        let days = item['left'] as number;
        let daysString = days == 0 ? '' : `${days} day${days > 1 ? 's' : ''} left`;
        console.log(item);

        items.push({
          actualPrice: item['actualPrice'],
          discount: item['discount'],
          left: daysString,
          name: item['name'],
          price: item['price'],
          imageUrl: this.sanitizer.bypassSecurityTrustResourceUrl(item['imageUrl']) as string,
          productId: item['productId'],
        });
      }
      this.items = items;
    }, error => console.error(error), () => { this.loading = false})
  }

  toDetail(productId) {
    console.log()
    if (productId) {
      this.router.navigate(["/app-detail", productId]);
    }
  }

  ngOnInit(): void {
      
  }
}
