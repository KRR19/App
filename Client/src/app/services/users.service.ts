import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {UserInfoModel} from '../shared/models/user-info.model';
import {RolesModel} from '../shared/models/roles.model';
import {ChangeRoleModel} from '../shared/models/change-role.model';

@Injectable()
export class UsersService {
  private Api = 'api';
  private control = 'User';
  private ActionChangeRole = 'ChangeRole';
  private ActionGetAll = 'GetAll';
  private ActionGetAllRoles = 'GetAllRoles';
  private serverUrl = `${environment.protocol}://${environment.host}:${environment.port}/${this.Api}/${this.control}`;

  constructor(private  http: HttpClient) {
  }

  public async GetAll(): Promise<UserInfoModel[]> {
    const urlPath = `${this.serverUrl}/${this.ActionGetAll}`;
    const result: UserInfoModel[] = await this.http.get<UserInfoModel[]>(urlPath).toPromise();
    return result;
  }

  public async GetAllRoles(): Promise<RolesModel[]> {
    const urlPath = `${this.serverUrl}/${this.ActionGetAllRoles}`;
    const result: RolesModel[] = await this.http.get<RolesModel[]>(urlPath).toPromise();
    return result;
  }

  public async ChangeRole(RoleModel: ChangeRoleModel) {
    const urlPath = `${this.serverUrl}/${this.ActionChangeRole}`;
    const result: ChangeRoleModel = await this.http.post<ChangeRoleModel>(urlPath, RoleModel).toPromise();
    return result;
  }
}
