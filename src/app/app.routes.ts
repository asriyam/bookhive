import { Routes } from '@angular/router';
import { MainLayout } from './layout/main-layout/main-layout';
import { ListLayout } from './layout/list-layout/list-layout';

export const routes: Routes = [
    {
        path: '',
        component: MainLayout,  // ← Layout wraps children
        children: [
            {
                path: '',
                loadComponent: () => import('./features/home/pages/home/home')
                    .then(m => m.Home),
            }
        ]
    },
    {
        path: 'search',
        component: ListLayout,
        children: [
            {
                path: '',
                loadComponent: () => import('./features/search/pages/search/search')
                    .then(m => m.Search)
            }
        ]
    },
    {
        path: 'review',
        component: ListLayout,
        children: [
            {
                path: '',
                loadComponent: () => import('./features/review/pages/user-books/user-books')
                    .then(m => m.UserBooks)
            }
        ]
    },
    {
        path: 'explore',
        component: ListLayout,
        children: [
            {
                path: '',
                loadComponent: () => import('./features/explorer/pages/content-explorer/content-explorer')
                    .then(m => m.ContentExplorer)
            }
        ]
    }
];
