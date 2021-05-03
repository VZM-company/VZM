import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  items = [{
    name: "qwer",
    price: 132,
    left: 10,
    discount: 101
  }, {
      name: "rewfasdv",
      price: 121,
      left: 10,
      discount: 1401
    }, {
      name: "aegaerg",
      price: 152,
      left: 10,
      discount: 101
    }, {
      name: "asfawer",
      price: 412,
      left: 103,
      discount: 1601
    }]
  constructor() {
  }
}
