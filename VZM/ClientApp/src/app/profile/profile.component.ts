import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

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

  profile = {
    name: "Lorem",
    description: "Lorem ipsum hey...",
  }

  constructor(

  ) {
    
  }

  ngOnInit(): void {
      
  }
}
