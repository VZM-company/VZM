import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { UserService, productModel } from '../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { AlertDialogComponent } from '../dialogs/alert-dialog/alert-dialog.component';

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
  
  apiUrl: string;
  product;

  constructor(
    //api: HttpService,
    private api: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog
  ) {
    this.api = api;
    this.dialog = dialog;
    this.baseUrl = baseUrl;
    this.userService = userService;
    this.apiUrl = this.baseUrl + 'api/products';
    this.router = router;
    
    activatedRoute.params.subscribe(params => {
      let id = params['id'];
      if (id == "create") {
        // creating
        this.product = { Description: "", DescriptionShort: "", ImageUrl: "", MetaTitle: "", Price: 0, ProductId: "", Title: "", SellerId: "" };
        this.product.ProductId = "00000000-0000-0000-0000-000000000000";

        this.productForm.get("descriptionShort").setValue(this.product.DescriptionShort);
        this.productForm.get("description").setValue(this.product.Description);
        this.productForm.get("metaTitle").setValue(this.product.MetaTitle);
        this.productForm.get("imageUrl").setValue(this.product.ImageUrl);
        this.productForm.get("price").setValue(this.product.Price);
        this.productForm.get("title").setValue(this.product.Title);
      } else {
        // updating
        this.api.get(this.apiUrl + '/' + id).subscribe(result => {
          this.product = {
            Description: result['description'],
            DescriptionShort: result['descriptionShort'],
            ImageUrl: result['imageUrl'],
            MetaTitle: result['metaTitle'],
            Price: result['price'],
            ProductId: result['productId'],
            Title: result['title'],
            SellerId: result['sellerId'],
          };
          this.productForm.get("descriptionShort").setValue(this.product.DescriptionShort);
          this.productForm.get("description").setValue(this.product.Description);
          this.productForm.get("metaTitle").setValue(this.product.MetaTitle);
          this.productForm.get("imageUrl").setValue(this.product.ImageUrl);
          this.productForm.get("price").setValue(this.product.Price);
          this.productForm.get("title").setValue(this.product.Title);
        }, error => console.error(error));
      }
    })
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


      let option = this.product.ProductId.trim() == '00000000-0000-0000-0000-000000000000' ? "create" : "update";
      console.log(this.product);
      console.log(this.userService.user);
      if (option == 'create') {
        this.api.post(this.apiUrl + '/create', this.product).subscribe(result => {
          //console.log("result of create", result);
          let ref = this.dialog.open(AlertDialogComponent, { data: { title: "Product action", description: "Product was successfully created!" } })
          ref.afterClosed().subscribe(res => {
            this.router.navigate([".."]);
          })
        }, error => console.error(error));
      } else if (option == 'update') {
        this.api.put(this.apiUrl + '/update', this.product).subscribe(result => {
          //console.log("result of update", result);
          let ref = this.dialog.open(AlertDialogComponent, { data: { title: "Product action", description: "Product was successfully updated!" } })
          ref.afterClosed().subscribe(res => {
            this.router.navigate([".."]);
          })
        }, error => console.error(error));
      }
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
