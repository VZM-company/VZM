import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { UserService } from '../services/user.service';
import { ActivatedRoute } from '@angular/router';

interface productModel {
  ProductId: string,
  UserId: string,
  Title: string,
  MetaTitle: string,
  Price: number,
  Description: string,
  DescriptionShort: string,
  Image: string,
};

@Component({
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  productForm = new FormGroup({
    userName: new FormControl(""),
    metaTitle: new FormControl(""),
    title: new FormControl(""),
    price: new FormControl(""),
    description: new FormControl(""),
    descriptionShort: new FormControl(""),
    image: new FormControl(""),
  });
  
  api: HttpClient;
  baseUrl: string;
  userService: UserService;
  apiUrl: string;
  product: productModel;


  constructor(
    //api: HttpService,
    api: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    userService: UserService,
    router: ActivatedRoute
  ) {
    this.api = api;
    this.baseUrl = baseUrl;
    this.userService = userService;
    this.apiUrl = this.baseUrl + 'api/products';
    
    router.params.subscribe(params => {
      let id = params['id'];
      if (id == "create") {
        // creating
      } else {
        // updating
        this.product.ProductId = id;
      }
    })

    this.product = { Description: "", DescriptionShort: "", Image: "", MetaTitle: "", Price: 0, ProductId: "", Title: "", UserId: "" };
    this.productForm.get("descriptionShort").setValue("descriptionShort");
    this.productForm.get("description").setValue("description of product");
    this.productForm.get("metaTitle").setValue("metaTitle of product");
    this.productForm.get("image").setValue("image of product");
    this.productForm.get("price").setValue("122");
    this.productForm.get("title").setValue("title of product");
  }

  save() {

    if (this.productForm.valid) {
      this.product.DescriptionShort = this.productForm.get("descriptionShort").value;
      this.product.Description = this.productForm.get("description").value;
      this.product.MetaTitle = this.productForm.get("metaTitle").value;
      this.product.Image = this.productForm.get("image").value;
      this.product.Price = this.productForm.get("price").value;
      this.product.Title = this.productForm.get("title").value;
      this.product.UserId = this.userService.user["userId"];

      let option = this.product.ProductId.trim() == '' ? "/create" : "/update";
      console.log(this.product);
      console.log(this.userService.user);
      this.api.post(this.apiUrl + option, { ...this.product, /*user: this.userService.user*/ }).subscribe(result => {
        console.log(result);
        //this.userService.setUser(result)
      }, error => console.error(error));
    }
  }


  ngOnInit(): void {
      
  }
}
