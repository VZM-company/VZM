import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
//import { HttpService } from '../core/http.service';

interface userModel {
  login: string;
  
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
    nickname: new FormControl(""),
    email: new FormControl(""),
    password: new FormControl(""),
    repeat_password: new FormControl(""),
    type: new FormControl(""),
  });


  //api: HttpService;
  api: HttpClient;
  baseUrl : string;

  constructor(
    //api: HttpService,
    api: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.api = api;
    this.baseUrl = baseUrl;
  }

  login() {
    let userName = this.loginForm.get("userName").value;
    let password = this.loginForm.get("password").value;
    let url = this.baseUrl + 'user';
    //this.api.get<{}[]>(this.baseUrl + "weatherforecast").subscribe(result => {
    //  console.log(result)
    //}, error => console.error(error));

    //console.log(this.baseUrl + "weatherforecast")
    //this.api.post<{}[]>(this.baseUrl + "weatherforecast", {userName: "qweqwe"}).subscribe(result => {
    //  console.log(result)
    //}, error => console.error(error));

    this.api.post<{}[]>(url, { "userName": userName, "pasword": password }).subscribe(result => {
      console.log(result);
    }, error => console.error(error));
    //this.api.send(url, {}, { "userName": userName, "pasword": password }).then(res => {
    //  console.log(res);
    //}).catch(res => {
    //  console.log(res);
    //})
  }

  ngOnInit(): void {
      
  }
}
