import { Injectable, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class PostCatalogService
{
    httpAuthAttr;
    tokenValue: string;
    baseURL: string;

    constructor(private cookieservice: CookieService,
                @Inject('BASE_URL') baseUrl: string, 
                private http: HttpClient)
    {
        this.baseURL = baseUrl;
        this.tokenValue = this.cookieservice.get('token');

        this.httpAuthAttr = {
            headers: new HttpHeaders({
                'Content-Type':  'application/json',
                'Authorization': 'Bearer ' + this.tokenValue
            })
        };
    }

    getUserPostsRange(from: number, to: number)
    {
        return this.http.get(this.baseURL + 'api/posts/' + from + '-' + to, this.httpAuthAttr);
    }

    getPostPhoto(id: number)
    {
        return this.http.get(this.baseURL + 'api/posts/' + id + '/photo', this.httpAuthAttr);
    }

    addNewPost(form: FormData)
    {
        return this.http.post(this.baseURL + 'api/posts', form, this.httpAuthAttr);
    }
}