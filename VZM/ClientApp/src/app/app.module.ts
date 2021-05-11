import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatCardModule, MatDialogModule, MatFormFieldControl, MatFormFieldModule, MatIconModule, MatInputModule, MatProgressSpinner, MatProgressSpinnerModule, MatSelectModule, MatTabsModule } from '@angular/material';
import { AppDetailComponent } from './app-detail/app-detail';
import { AuthComponent } from './auth/auth.component'
import { CartComponent } from './cart/cart.component';
import { ProfileComponent } from './profile/profile.component';
import { SearchComponent } from './search/search.component';
import { UserService } from './services/user.service';
import { AppStorageService } from './core/app-storage.service';
import { ProductComponent } from './product/product.component';
import { AlertDialogComponent } from './dialogs/alert-dialog/alert-dialog.component';
import { ConfirmDialogComponent } from './dialogs/confirm-dialog/confirm-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AppDetailComponent,
    AuthComponent,
    CartComponent,
    ProfileComponent,
    SearchComponent,
    ProductComponent,

    AlertDialogComponent,
    ConfirmDialogComponent
  ],
  entryComponents: [
    AlertDialogComponent,
    ConfirmDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'app-detail/:id', component: AppDetailComponent },
      { path: 'auth', component: AuthComponent },
      { path: 'cart', component: CartComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'profile/:id', component: ProductComponent },
      { path: 'search', component: SearchComponent },
    ]),
    BrowserAnimationsModule,
    MatButtonModule,
    MatCardModule,
    MatTabsModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDialogModule,
  ],
  providers: [UserService, AppStorageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
