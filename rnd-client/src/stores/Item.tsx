import {makeAutoObservable} from "mobx";
import Provider from "../data/Provider";
import Result from "../data/Result";
import Message from "../data/Message";

export default class Item<T> {
  load(): void {
    this.loaded = false;
    this.failed = false;
    this.message = new Message({title: ""});;
    this.data = null;

    this.provider.get(this.identifier).then(this.loadSuccess);
  }

  loadSuccess(result: Result<T>): void {
    this.loaded = true;
    this.failed = !result.success;
    this.message = result.message;
    this.data = new this.type(result.data);
  }

  loaded: boolean
  failed: boolean
  message: Message
  data: T | null

  readonly provider : Provider<T>
  readonly identifier: string
  readonly type : new(data: any) => T

  constructor(provider : Provider<T>, identifier: string, type: new(data: any) => T) {
    this.loaded = false;
    this.failed = false;
    this.message = new Message({title: ""});;
    this.data = null;

    this.provider = provider;
    this.identifier = identifier;
    this.type = type;

    makeAutoObservable(this, {
      provider: false,
      identifier: false,
      type: false,
    }, { autoBind: true })
  }
}