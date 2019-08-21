import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { AuthService} from './authentification.service';

import { UserToLogin } from 'src/app/dto/UserToLogin';
import { UserToSignup } from 'src/app/dto/UserToSignup';

import {Router} from '@angular/router';

@Component({
    selector: 'login-form',
    templateUrl: './login-form.component.html',
    styleUrls: ['./login-form.component.css'],
    providers: [AuthService, CookieService]
})
export class LoginFormComponent {
    formIsValid = true;
    isLoginState = true;
    isRequestSent = false;
    errorMessage = '';
    userToLogin: UserToLogin = new UserToLogin();
    userToSignup: UserToSignup = new UserToSignup();

    constructor(private authService: AuthService,
                private router: Router) {}

    Login()
    {
        this.isRequestSent = true;
        this.errorMessage = '';
        this.authService.Login(this.userToLogin).subscribe(
            (success) => {
                this.router.navigate(['/'])
                this.isRequestSent = false;
            },
            (error) => {
                this.handleServerError(error.status);
                this.isRequestSent = false;
            }
        );
    }

    Signup()
    {
        this.errorMessage = '';
        this.authService.SignUp(this.userToSignup).subscribe(
            (success) => {
                this.router.navigate(['/login'])
                this.isRequestSent = false;
            },
            (error) => {
                this.handleServerError(error.status);
                this.isRequestSent = false;
            }
        );
    }

    switchModes() { this.isLoginState = !this.isLoginState; }

    handleServerError(statusCode: number,): void
    {
        switch (statusCode)
        {
            case 400:
                this.errorMessage = "Wrong username or password!";
                break;
            case 500:
                this.errorMessage = "Wrong username or password!";
                break;
            default:
                console.error("Server returned code: ${statusCode}");
        }
    }
}