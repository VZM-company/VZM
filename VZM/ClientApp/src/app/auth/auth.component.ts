import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { UserService } from '../services/user.service';
import { MatDialog } from '@angular/material';
import { AlertDialogComponent } from '../dialogs/alert-dialog/alert-dialog.component';
import { Router } from '@angular/router';

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

  apiUrl: string;

  constructor(
    //api: HttpService,
    private api: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private userService: UserService,
    private dialog: MatDialog,
    private router: Router,
  ) {
    this.api = api;
    this.baseUrl = baseUrl;
    this.userService = userService;
    this.apiUrl = this.baseUrl + 'api/user';
    this.dialog = dialog;
    this.router = router;

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
        if (result == null) {
          this.dialog.open(AlertDialogComponent, { data: { title: "Authentication error", description: "Wrong credentials" } });
        } else {
          this.router.navigate(['/']);
        }
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
        let ref = this.dialog.open(AlertDialogComponent, { data: { title: "Authentication", description: "You've registrated successfully!" } });;
        ref.afterClosed().subscribe(() => {
          this.router.navigate(["/"]);
        })
        this.userService.setUser(result)
      }, error => console.error(error));
    }
  }


  ngOnInit(): void {
      
  }
}
