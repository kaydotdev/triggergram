import { Component, AfterContentChecked } from '@angular/core';
import {Router} from '@angular/router';

import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'login-bar',
  templateUrl: './login-bar.component.html',
  styleUrls: ['./login-bar.component.css'],
  providers: [CookieService]
})
export class LoginBarComponent implements AfterContentChecked {
    isUserLogined: boolean;

    constructor(private cookieservice: CookieService,
                private router: Router)
    {
        this.isUserLogined = this.cookieservice.check('token');
    }

    ngAfterContentChecked() { this.isUserLogined = this.cookieservice.check('token'); }

    Login() {  this.router.navigate(['/login']); }

    Logout() { 
        this.cookieservice.delete('token'); 
        this.isUserLogined = false;
        this.router.navigate(['/login']);
    }
}