import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
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

  items_other = this.items;

  constructor(
    private userService: UserService,
    private api: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
  ) {
    
  }

  ngOnInit(): void {
      
  }
}
