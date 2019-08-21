import { Component } from '@angular/core';
import { PostCatalogService } from './post-catalog.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { PostPreview } from 'src/app/dto/PostPreview';
import { PhotoPost } from 'src/app/dto/PhotoPost';

@Component({
    selector: 'post-catalog',
    templateUrl: './post-catalog.component.html',
    providers: [PostCatalogService, CookieService]
})
export class PostCatalogComponent {
    startIndex = 0;
    catalogPhotoAmmoutWidth = 3;
    catalogPhotoLoadingStep = 9;
    currentUser: string;

    postPreview: PostPreview[] = [];
  
    constructor(private postCatalogService: PostCatalogService,
                private cookieService: CookieService,
                private router: Router) {
        this.currentUser = this.cookieService.get("username");
        this.getPostPreviewRange(this.startIndex, this.startIndex + this.catalogPhotoLoadingStep);
    }

    getPostPreviewRange(from: number, to: number)
    {
        //this.postPreview = [];

        this.postCatalogService.getUserPostsRange(from, to).subscribe(
            (data) => {
                let PhotoData: PhotoPost[] = Array.prototype.slice.call(data);
                console.log(this.postPreview);

                for (let i = 0; i < PhotoData.length; i++)
                {
                    this.postPreview.push(new PostPreview());
                    this.postPreview[i].id = PhotoData[i].id;

                    this.postCatalogService.getPostPhoto(PhotoData[i].id).subscribe(
                        (data) => {
                            let postHeader = {
                                id:1, 
                                name:"", 
                                source:""
                            };
                            Object.assign(postHeader, data);
                            this.postPreview[i].photoSource = postHeader.source;
                        },
                        (error) => this.handleServerError(error.status)
                    );
                }

                console.log(this.postPreview);
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