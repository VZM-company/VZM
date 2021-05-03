import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './app-detail.html',
  styleUrls: ['./app-detail.css']
})
export class AppDetailComponent implements OnInit {
  app: {
    title: string,
    description: string,
    price: number,
    discount: number,
    images: string[],
    actualPrice: number
  };

  slides: string[];
  currentSlide: number;

  getSlide() {
    return this.slides[this.currentSlide];
  }

  getPrev() {
    this.currentSlide = this.currentSlide === 0 ? 0 : this.currentSlide - 1;
  }
  
  getNext() {
    this.currentSlide = this.currentSlide === this.slides.length ? this.currentSlide : this.currentSlide + 1;
  }

  constructor(

  ) {
    this.app = {
      description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
      discount: 5,
      images: ['qwerqwer.png', 'tqertqw.png', 'eqwerqwer.png', 'dqwerqwer.png',],
      price: 120,
      title: "This War of Mine",
      actualPrice: 120 - ((5 / 100) * 120)
    }
  }


  ngOnInit(): void {
      
  }
}
