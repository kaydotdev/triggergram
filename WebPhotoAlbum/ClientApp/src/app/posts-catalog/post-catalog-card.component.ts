import { Component, Input, OnInit } from '@angular/core';
import { PostCatalogService } from './post-catalog.service';
import { CookieService } from 'ngx-cookie-service';
import { PostPreview } from 'src/app/dto/PostPreview';

@Component({
    selector: 'post-catalog-card',
    templateUrl: './post-catalog-card.component.html',
    providers: [PostCatalogService, CookieService]
})
export class PostCatalogCardComponent{
    @Input() postPreview: PostPreview;
    @Input() index: number;

    constructor() {}
}