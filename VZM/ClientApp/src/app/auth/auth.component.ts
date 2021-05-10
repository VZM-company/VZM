import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { UserService } from '../services/user.service';

interface userModel {
  UserName: string;
  PasswordHash: string;
  Email: string;
  Role: string;
  Name: string;
  ImageUrl: string;
}

@Component({
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  loginForm = new FormGroup({
    userName: new FormControl(""),
    password: new FormControl(""),
  });

  registerForm = new FormGroup({
    username: new FormControl(""),
    email: new FormControl(""),
    password: new FormControl(""),
    repeat_password: new FormControl(""),
    role: new FormControl(""),
    name: new FormControl(""),
    imageUrl: new FormControl(""),
  });

  
  api: HttpClient;
  baseUrl: string;
  userService: UserService;
  apiUrl: string;

  constructor(
    //api: HttpService,
    api: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    userService: UserService
  ) {
    this.api = api;
    this.baseUrl = baseUrl;
    this.userService = userService;
    this.apiUrl = this.baseUrl + 'api/user';

    this.registerForm.get("username").setValue("username test");
    this.registerForm.get("password").setValue("123123");
    this.registerForm.get("repeat_password").setValue("123123");
    this.registerForm.get("imageUrl").setValue("");
    this.registerForm.get("role").setValue("customer");
    this.registerForm.get("email").setValue("tut@tut.tut");
    this.registerForm.get("name").setValue("user name test");
  }

  login() {
    let userName = this.loginForm.get("userName").value;
    let password = this.loginForm.get("password").value;

    if (this.loginForm.valid) {
      this.api.post(this.apiUrl + "/login", { "UserName": userName, "Password": password }).subscribe(result => {
        console.log(result);
        this.userService.setUser(result)
      }, error => console.error(error));
    }
  }

  deleteImage() {
    this.registerForm.get("imageUrl").setValue("");
  }

  uploadImage(file) {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.registerForm.get("imageUrl").setValue(reader.result);
    };
  }

  register() {
    let newUser: userModel = {
      UserName: this.registerForm.get("username").value,
      PasswordHash: this.registerForm.get("password").value,
      Role: this.registerForm.get("role").value,
      Email: this.registerForm.get("email").value,
      Name: this.registerForm.get("name").value,
      ImageUrl: this.registerForm.get("imageUrl").value,
    }

    if (this.registerForm.valid) {
      this.api.post(this.apiUrl + "/register", { ...newUser }).subscribe(result => {
        console.log(result);
        this.userService.setUser(result)
      }, error => console.error(error));
    }
  }


  ngOnInit(): void {
      
  }
}
