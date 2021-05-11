import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UserService } from '../services/user.service';

@Component({
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  items = [{
    name: "qwer",
    price: 132,
    actualPrice: 120,
    left: '12:21',
    discount: 10
  }, {
    name: "rewfasdv",
    price: 121,
    actualPrice: 115,
    left: '20:30',
    discount: 5
  }, {
    name: "aegaerg",
    price: 152,
    actualPrice: 135,
    left: '12:00',
    discount: 12
  }, {
    name: "asfawer",
    actualPrice: 460,
    price: 412,
    left: '10:11',
    discount: 11
    }];
  
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
    @Inject('BASE_URL') private baseUrl: string,
  ) {
    
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
      startPrice: this.searchForm.get("startPrice").value,
      category: this.searchForm.get("category").value,
      endPrice: this.searchForm.get("endPrice").value,
      title: this.searchForm.get("title").value,
    }).toString();
    
    this.api.get(this.baseUrl + 'api/products/find?' + queryString).subscribe(res => {
      console.log("result from api/products/find is ", res)
    }, error => console.error(error))
  }

  ngOnInit(): void {
      
  }
}
