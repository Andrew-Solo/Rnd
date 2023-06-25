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

export enum Prototype {
  Property = "Property",
  Expression = "Expression",
  Reference = "Reference"
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

export enum Enumerating {
  None = "None",
  Set = "Set",
  List = "List",
  Dictionary = "Dictionary",
}

export enum Accessibility {
  Space = "Space",
  Unit = "Unit",
  Module = "Module",
  Global = "Global",
}
