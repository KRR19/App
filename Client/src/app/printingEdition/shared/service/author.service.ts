import {Injectable} from '@angular/core';
import {AuthorModel} from '../../../models/AuthorModel';
import {HttpClient} from '@angular/common/http';

@Injectable()
export  class AuthorService {
  constructor(private  http: HttpClient) {}

  public async AddAuthor(model: AuthorModel) {
    console.log(model);
    const result: AuthorModel = await this.http.post<AuthorModel>('https://localhost:44378/api/Author/Create', model).toPromise();
    console.log(result);
    return result;
  }

  public async GetAll() {
    const authors: AuthorModel[] = await this.http.get<AuthorModel[]>('https://localhost:44378/api/Author/GetAll').toPromise();
    return authors;
  }

  public async GetById(author: string) {
    console.log(author)
    const authors: AuthorModel = await this.http.get<AuthorModel>('https://localhost:44378/api/Author/Get/' + author).toPromise();
    console.log(authors)
    return authors;
  }
}
