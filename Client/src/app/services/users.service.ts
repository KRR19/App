import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {UserInfoModel} from '../shared/models/user-info.model';
import {RolesModel} from '../shared/models/roles.model';
import {ChangeRoleModel} from '../shared/models/change-role.model';
import {ResponseModel} from '../shared/models/response.model';

@Injectable()
export class UsersService {
  private Api = 'api';
  private control = 'User';
  private ActionChangeRole = 'ChangeRole';
  private ActionGetAll = 'GetAll';
  private ActionGetAllRoles = 'GetAllRoles';
  private ActionGet = 'Get';
  private ActionDelete = 'Delete';
  private ActionUpdate = 'Update';


  constructor(private  http: HttpClient) {
  }

  public async GetAll(): Promise<UserInfoModel[]> {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.Api}/${this.control}/${this.ActionGetAll}`;
    const result: UserInfoModel[] = await this.http.get<UserInfoModel[]>(urlPath).toPromise();
    return result;
  }

  async GetAllRoles(): Promise<RolesModel[]> {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.Api}/${this.control}/${this.ActionGetAllRoles}`;
    const result: RolesModel[] = await this.http.get<RolesModel[]>(urlPath).toPromise();
    return result;
  }

  async ChangeRole(RoleModel: ChangeRoleModel) {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.Api}/${this.control}/${this.ActionChangeRole}`;
    const result: ChangeRoleModel = await this.http.post<ChangeRoleModel>(urlPath, RoleModel).toPromise();
    return result;
  }
}
