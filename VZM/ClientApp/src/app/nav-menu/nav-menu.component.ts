import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(
    private userService: UserService,
    private router: Router,
  ) {

  }

  get isAuthenticated(): boolean {
    return this.userService != null && this.userService.isAuthenticated;
  }

  get isSeller(): boolean {
    return this.userService != null && this.userService.isSeller;
  }

  logout() {
    this.userService.setUser(null);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
