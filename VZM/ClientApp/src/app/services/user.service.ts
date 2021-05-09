import { Injectable } from '@angular/core';
import { AppStorageService } from '../core/app-storage.service';

export interface productModel {
  ProductId: string,
  SellerId: string,
  Title: string,
  MetaTitle: string,
  Price: number,
  Description: string,
  DescriptionShort: string,
  ImageUrl: string,
};

@Injectable()
export class UserService {
    user: Object;
    private storeKey = '__user.service_';

    constructor (
        private storage: AppStorageService,
    ) {
        if (this.getStoreParam('access_token') != null) {
            this.user = this.getStoreParam('user');
        }
    }

    private getStoreParam (key: string) {
        return this.storage.getParam(this.storeKey + key);
    }

    private setStoreParam (key: string, value: any) {
        return this.storage.setParam(this.storeKey + key, value);
    }

    /**
     * Set user object.
     * @param userInfo the object.
     */
    setUser (userInfo: Object) {
        this.user = userInfo;
        this.setStoreParam('user', this.user);
    }

    /**
       * Set user object.
       * @param userInfo the object.
       */
    getUser() : Object {
      return this.getStoreParam('user');
    }

    /**
     * Whether the current user is a authenticated.
     */
    isAuthenticated (): boolean {
        if (this.getStoreParam('user') !== null) {
            return true;
        } else {
            return false;
        }
    }

    /**
     * Logs out the current user.
     */
    logout () {
        this.setStoreParam('user', null);
    }
}
