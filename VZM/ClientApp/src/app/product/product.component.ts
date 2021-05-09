import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { UserService, productModel } from '../services/user.service';
import { ActivatedRoute } from '@angular/router';

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
    imageUrl: new FormControl(""),
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

    this.product = { Description: "", DescriptionShort: "", ImageUrl: "", MetaTitle: "", Price: 0, ProductId: "", Title: "", SellerId: "" };
    this.productForm.get("descriptionShort").setValue("descriptionShort");
    this.productForm.get("description").setValue("description of product");
    this.productForm.get("metaTitle").setValue("metaTitle of product");
    this.productForm.get("imageUrl").setValue("");
    this.productForm.get("price").setValue("122");
    this.productForm.get("title").setValue("title of product");
  }

  save() {

    if (this.productForm.valid) {
      this.product.DescriptionShort = this.productForm.get("descriptionShort").value;
      this.product.Description = this.productForm.get("description").value;
      this.product.MetaTitle = this.productForm.get("metaTitle").value;
      this.product.ImageUrl = this.productForm.get("imageUrl").value;
      this.product.Price = this.productForm.get("price").value;
      this.product.Title = this.productForm.get("title").value;
      this.product.SellerId = this.userService.getUser()['userId'];
      this.product.ProductId = "00000000-0000-0000-0000-000000000000";


      let option = this.product.ProductId.trim() == '00000000-0000-0000-0000-000000000000' ? "/create" : "/update";
      console.log(this.product);
      console.log(this.userService.user);
      this.api.post(this.apiUrl + option, { ...this.product, /*user: this.userService.user*/ }).subscribe(result => {
        console.log(result);
        //this.userService.setUser(result)
      }, error => console.error(error));
    }
  }

  deleteImage() {
    this.productForm.get("imageUrl").setValue("");
  }

  uploadImage(file) {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.productForm.get("imageUrl").setValue(reader.result);
    };
  }

  ngOnInit(): void {
      
  }
}
