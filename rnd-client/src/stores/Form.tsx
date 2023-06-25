import {makeAutoObservable, reaction} from "mobx";
import Message from "../data/Message";
import Item from "./Item";
import Instance from "./models/Instance";
import Collection from "./Collection";
import Field from "./models/Field";
import {client} from "../data/Client";
import {store} from "./Store";
import Unit from "./models/Unit";

export default class Form {
  loaded: boolean
  failed: boolean
  message: Message
  data: {[name: string]: any}

  editing: boolean

  fields: Collection<Field>
  instance: Item<Instance>

  constructor(unit: Unit, identifier: string) {
    this.loaded = false;
    this.failed = false;
    this.message = new Message({title: ""});
    this.data = {};

    this.editing = false;

    makeAutoObservable(this, {
      fields: false,
      instance: false
    }, { autoBind: true })

    this.fieldsSync = reaction(
      () => this.fields.loaded,
      loaded => {
        this.loaded = this.instance.loaded && loaded;
        if (this.loaded) {
          this.failed = this.instance.failed || this.fields.failed;
          this.message = this.fields.message;
        }
      }
    )

    this.instanceSync = reaction(
      () => this.instance.loaded,
      loaded => {
        this.loaded = this.fields.loaded && loaded;
        if (this.loaded) {
          this.failed = this.instance.failed || this.fields.failed;
          this.message = this.instance.message;
          this.data = {...this.instance.data, properties: undefined, ...this.instance.data?.properties};
        }
      }
    )

    this.fields = unit.fields;
    this.instance = new Item<Instance>(client.instances(store.user, unit.path), identifier, Instance);
    this.instance.load();
  }

  fieldsSync
  instanceSync

  dispose() {
    this.fieldsSync()
    this.instanceSync()
  }
}