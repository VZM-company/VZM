import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UserService, productModel } from '../services/user.service';

@Component({
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  items = [{
    name: "qwer",
    price: 132,
    actualPrice: 120,
    left: '12:21',
    discount: 10,
    image: "",
  }, {
    name: "rewfasdv",
    price: 121,
    actualPrice: 115,
    left: '20:30',
      discount: 5,
      image: "",
  }, {
    name: "aegaerg",
    price: 152,
    actualPrice: 135,
    left: '12:00',
      discount: 12,
      image: "",
  }, {
    name: "asfawer",
    actualPrice: 460,
    price: 412,
    left: '10:11',
      discount: 11,
      image: "",
    }];

  profile = {
    name: "Lorem",
    description: "Lorem ipsum hey...",

  }

  api: HttpClient;
  baseUrl: string;
  userService: UserService;
  apiUrl: string;
  products: productModel[];

  constructor(
    userService: UserService,
    router: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string,
    api: HttpClient,
  ) {
    this.api = api;
    this.baseUrl = baseUrl;
    this.userService = userService;
    this.apiUrl = this.baseUrl + 'api/user';

    this.api.post(this.apiUrl + '/products', { "userId": this.userService.getUser()['userId']}).subscribe(result => {
      console.log(result);
      this.items = [];
      for (let item of result as []) {
        this.items.push({
          //actualPrice: item['price'],
          //discount: item['price'],
          //left: item['price'],
          actualPrice: 11,
          discount: 10,
          left: "10:10",
          name: item['title'],
          price: item['price'],
          image: item['image'],
        })
      }
    }, error => console.error(error));
  }

  ngOnInit(): void {
      
  }
}
