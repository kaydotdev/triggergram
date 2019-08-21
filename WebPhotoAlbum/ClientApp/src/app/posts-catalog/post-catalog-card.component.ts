import { Component, Input, OnInit } from '@angular/core';
import { PostCatalogService } from './post-catalog.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { PostPreview } from 'src/app/dto/PostPreview';
import { Photo } from 'src/app/dto/Photo';

@Component({
    selector: 'post-catalog-card',
    templateUrl: './post-catalog-card.component.html',
    providers: [PostCatalogService, CookieService]
})
export class PostCatalogCardComponent implements OnInit{
    photoSource: string;
    postPreview: PostPreview = new PostPreview();

    @Input() postId: number;

    constructor(private postCatalogService: PostCatalogService,
                private router: Router) {}

    ngOnInit(): void {
        this.postCatalogService.getPostPhoto(this.postId).subscribe(
            (data) => {
                let PhotoData: Photo;
                Object.assign(PhotoData, data);
                this.photoSource = PhotoData.source;
            },
            (error) => this.handleServerError(error.status)
        );
    }

    handleServerError(statusCode: number): void
    {
        switch (statusCode) 
        {
            case 401:
                this.router.navigate(['/login']);
                break;
            default:
                console.error("Server returned code: ${statusCode}");
        }
    }
}