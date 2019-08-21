import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserToLogin } from 'src/app/dto/UserToLogin';
import { UserToSignup } from 'src/app/dto/UserToSignup';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class AuthService
{
    baseURL: string;
    httpAuthAttr = { headers: new HttpHeaders({ 'Content-Type':  'application/json' }) };

    constructor(private cookieService: CookieService,
                private http: HttpClient, 
                @Inject('BASE_URL') baseUrl: string)
    {
        this.baseURL = baseUrl;
    }

    SignUp(user: UserToSignup)
    {
        const body = {
            username: user.username,
            password: user.password,
            passwordconfirmation: user.passwordconfirmation
        };

        return this.http.post(this.baseURL + 'api/auth/signup', body);
    }

    Login(user: UserToLogin)
    {
        const body = {
            username: user.username,
            password: user.password
        };
        
        this.cookieService.set("username", user.username);
        return this.http.post(this.baseURL + 'api/auth/login', body);
    }
}