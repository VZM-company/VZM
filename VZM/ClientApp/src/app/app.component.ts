import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  constructor(
    private userService: UserService,
    private api: HttpClient,
    private router: Router,
    private sanitizer: DomSanitizer,
    @Inject('BASE_URL') private baseUrl: string,
  ) {
    if (this.userService.isAuthenticated) {
      let user = this.userService.getUser();
      this.api.post(this.baseUrl + "api/user/login", { "UserName": user['userName'], "Password": user['passwordHash'] }).subscribe(result => {
        router.navigate(["/"]);
      }, error => console.error(error));
    }
  }
}
