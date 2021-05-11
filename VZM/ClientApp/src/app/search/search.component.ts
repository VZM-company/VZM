import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

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

  filterForm = new FormGroup({
    from: new FormControl(""),
    to: new FormControl(""),
    category: new FormControl(""),
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

  ) {
    
  }

  ngOnInit(): void {
      
  }
}
