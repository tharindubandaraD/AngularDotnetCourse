import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in Successfully');
    }, error => {
        this.alertify.error('Fail to login');
    }, () => {
      this.router.navigate(['/members']);
    }
    );
  }

  // simply check the local storage and set true false
  loggedIn() {
    /*
    // this token part need to move to service because this is type of service -this has authority
    if we need to check status log in so we need to inject nav component
    const token = localStorage.getItem('token');
    return !!token;
     */
    return this.authService.loggedIn();
  }
 // token will store in local storage
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }


}
