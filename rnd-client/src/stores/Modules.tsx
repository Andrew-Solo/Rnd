import {makeAutoObservable} from "mobx";
import Store from "./Store";
import Module from "../models/Module";
import {client} from "../data/Client";


export default class Modules {
  constructor(store: Store) {
    this.store = store;
    this.data = [];

    this.syncModules();

    makeAutoObservable(this, {
      store: false,
    })
  }

  syncModules() {
    const userId = this.store.session.user?.id;
    const {data} = client.modules(userId ?? "@default").list();
    if (data !== null) this.data = data;
  }

  readonly store: Store
  data: Module[]
}