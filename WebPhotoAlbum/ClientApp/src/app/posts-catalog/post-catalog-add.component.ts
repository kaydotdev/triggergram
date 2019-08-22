import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PostCatalogService } from './post-catalog.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { PostPreview } from 'src/app/dto/PostPreview';

@Component({
    selector: 'post-catalog-add',
    templateUrl: './post-catalog-add.component.html',
    providers: [FormBuilder, PostCatalogService, CookieService]
})
export class PostCatalogAddComponent implements OnInit {
    dataMessage = '';
    errorMessage = '';
    postPreview: PostPreview[] = [];
    uploadForm: FormGroup;  
  
    constructor(private formBuilder: FormBuilder,
                private postCatalogService: PostCatalogService,
                private router: Router) {
        this.uploadForm = this.formBuilder.group({
            description: [''],
            photo: ['']
        });
    }

    onFileSelected(event)
    {
        this.clearMessages();
        if (event.target.files.length > 0) {
            const file = event.target.files[0];
            this.uploadForm.get('photo').setValue(file);
        }
    }

    clearMessages()
    {
        this.errorMessage = '';
        this.dataMessage = '';
    }

    onDescriptionChanged(event)
    {
        this.clearMessages();
        this.uploadForm.get('description').setValue(event.target.value);
    }

    submitPost()
    {
        this.clearMessages();
        let formData = new FormData();

        formData.append('photoFile', this.uploadForm.get('photo').value);
        formData.append('postDescription', this.uploadForm.get('description').value);

        this.postCatalogService.addNewPost(formData).subscribe(
            (res) => this.dataMessage = 'New post was successfully added!',
            (error) => this.handleServerError(error.status)
        );
    }

    ngOnInit(): void {
        this.clearMessages();
    }

    handleServerError(statusCode: number): void
    {
        switch (statusCode) 
        {
            case 400:
                this.errorMessage = 'Invalid input data!';
                break;
            case 401:
                this.router.navigate(['/login']);
                break;
            case 500:
                this.errorMessage = 'Oops! Something went wrong!';
                break;
            default:
                console.error("Server returned code: ${statusCode}");
        }
    }
}