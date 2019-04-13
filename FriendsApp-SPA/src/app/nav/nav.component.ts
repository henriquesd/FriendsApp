import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('Logged in successfully');
    }, error => {
      console.log(error);
    });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    // if there is something in this token then it's gonna return true, if this token variable is empty then it's gonna return false;
    return !!token;
  }

  logout() {
    localStorage.removeItem('token');
    console.log('Logget out');
  }

}
