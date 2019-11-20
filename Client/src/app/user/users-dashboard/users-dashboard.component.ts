import {Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator, MatTableDataSource} from '@angular/material';
import {UserInfoModel} from '../../shared/models/user-info.model';
import {UsersService} from '../../services/users.service';
import {ChangeRoleModel} from '../../shared/models/change-role.model';
import {RolesModel} from '../../shared/models/roles.model';

@Component({
  selector: 'app-users-dashboard',
  templateUrl: './users-dashboard.component.html',
  styleUrls: ['./users-dashboard.component.scss']
})
export class UsersDashboardComponent implements OnInit {
  private Users: UserInfoModel[] = [];
  private RoleModel: ChangeRoleModel = {};
  private dataSource = new MatTableDataSource<UserInfoModel>();
  private displayedColumns: string[] = ['Email', 'FirstName', 'SecondName', 'Role'];
  private roles: RolesModel[] = [];
  private edit: boolean;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(private userService: UsersService) { }

  async ngOnInit() {
    this. edit = false;
    this.roles = await this.userService.GetAllRoles();
    this.Users = await this.userService.GetAll();
    this.dataSource = new MatTableDataSource<UserInfoModel>(this.Users);
    this.dataSource.paginator = this.paginator;
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }


  Selectuser(element) {
    this.RoleModel.id = element.id;
    this.RoleModel.name = element.email;
    this.RoleModel.role = element.role;
    this.edit = true;
  }

  ChangeRole() {
    this.userService.ChangeRole(this.RoleModel).then(() => {window.location.reload(); } );

  }

  Cancel() {
    this.RoleModel = {};
    this.edit = false;
  }
}
