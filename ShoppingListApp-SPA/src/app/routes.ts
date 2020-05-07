import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { BasketListComponent } from './baskets/basket-list/basket-list.component';
import { BasketListResolver } from './_resolvers/basket-list.resolver';
import { ItemListComponent } from './baskets/item-list/item-list.component';
import { ItemListResolver } from './_resolvers/item-list.resolver';
import { BasketEditComponent } from './baskets/basket-edit/basket-edit.component';
import { BasketEditResolver } from './_resolvers/basket-edit.resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'baskets', component: BasketListComponent, resolve: {baskets: BasketListResolver } },
            { path: 'baskets/edit/:id', component: BasketEditComponent, resolve: {basket: BasketEditResolver},
                                    canDeactivate: [PreventUnsavedChanges] },
            { path: 'items', component: ItemListComponent, resolve: {items: ItemListResolver } },
            { path: 'members', component: MemberListComponent, resolve: {users: MemberListResolver } },
            { path: 'members/:id', component: MemberDetailComponent, resolve: {user: MemberDetailResolver} },
            { path: 'member/edit', component: MemberEditComponent, resolve: {user: MemberEditResolver},
                                   canDeactivate: [PreventUnsavedChanges] }
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
