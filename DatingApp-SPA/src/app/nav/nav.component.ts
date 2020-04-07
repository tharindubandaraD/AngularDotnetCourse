import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in Successfully');
    }, error => {
        this.alertify.error('Fail to login');
    });
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
    this.alertify.message('logged out');
  }


}
