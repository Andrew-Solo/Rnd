export enum Role {
  Viewer = "Viewer",
  Editor = "Editor",
  Moderator = "Moderator",
  Admin = "Admin",
  Owner = "Owner"
}

export interface Association {
  provider: string,
  identifier: string,
  secret: string | null
}

export enum Type {
  Object = "Object",
  Reference = "Reference",
  Procedure = "Procedure",
  String = "String",
  Integer = "Integer",
  Decimal = "Decimal",
  Boolean = "Boolean",
  Select = "Select",
  Color = "Color",
  Icon = "Icon",
  Image = "Image",
  DateTime = "DateTime",
  Date = "Date",
  Time = "Time",
  Link = "Link",
}

export enum Accessibility {
  Space = "Space",
  Unit = "Unit",
  Module = "Module",
  Global = "Global",
}

export enum Interactivity {
  Editable = "Editable",
  Readonly = "Readonly",
  Hidden = "Hidden",
  System = "System",
  Modifiable = "Modifiable",
}

export enum Enumerating {
  None = "None",
  Set = "Set",
  List = "List",
  Dictionary = "Dictionary",
}