import { Component } from '@angular/core';
import { TagSearchCatalogService } from './tag-search-catalog.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { PostPreview } from 'src/app/dto/PostPreview';
import { PhotoPost } from 'src/app/dto/PhotoPost';

@Component({
    selector: 'tag-search-catalog',
    templateUrl: './tag-search-catalog.component.html',
    styleUrls: ['./tag-search-catalog.component.css'],
    providers: [TagSearchCatalogService, CookieService]
})
export class TagSearchCatalogComponent {
    searchTag = '';
    startIndex = 0;
    catalogPhotoAmmoutWidth = 3;
    catalogPhotoLoadingStep = 9;

    isInCatalog = true;
    indexOfViewPost = 0;

    postPreview: PostPreview[] = [];
  
    constructor(private tagSearchCatalogService: TagSearchCatalogService,
                private router: Router) {
    }

    search()
    {
        this.getPostPreviewRange(this.startIndex, this.startIndex + this.catalogPhotoLoadingStep);
    }

    viewPost(index: number)
    {
        this.isInCatalog = false;
        this.indexOfViewPost = index;
    }

    onChanged(signal: any)
    {
        this.isInCatalog = true;
        this.indexOfViewPost = 0;
    }

    getPostPreviewRange(from: number, to: number)
    {
        this.tagSearchCatalogService.getTagPostsRange(this.searchTag, from, to).subscribe(
            (data) => {
                let PhotoData: PhotoPost[] = Array.prototype.slice.call(data);
                console.log(this.postPreview);

                for (let i = 0; i < PhotoData.length; i++)
                {
                    this.postPreview.push(new PostPreview());
                    this.postPreview[i].id = PhotoData[i].id;

                    this.tagSearchCatalogService.getPostPhoto(PhotoData[i].id).subscribe(
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
                console.log("Server returned code: " + statusCode);
        }
    }
}