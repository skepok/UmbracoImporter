module UImporter.Models {

    export class Meta {
        public Title: string;
        public Description: string;
        public Version: string;
    }
    export class Environment {
        public Name: string;
        public Host: string;
        public ConnectionString: string;
    }
    export class Config {
        public Mentor: string;
        public Meta: Meta;
        public Environments: Array<Environment>;
    }
    export class NodeType {
        public Name: string;
        public Ref: string;
    }
    export class StartNode {
        public Ref: string;
    }
    export class Item {
        public Name: string;
        public Alias: string;
        public Tabs: Array<Tab>;
        public Permissions: Permissions;
        public NodeType: NodeType;
        public StartNode: StartNode;
        public Items: Array<Item>;
    }
    export class DataTypes {
        public Items: Array<Item>;
    }
    export class Permissions {
        public AllowRoot: boolean;
    }
    export class Property {
        public Name: string;
        public NodeType: NodeType;
        public Mandatory: boolean;
        public Alias: string;
    }
    export class Tab {
        public Name: string;
        public Properties: Array<Property>;
    }
    export class DocumentTypes {
        public Items: Array<Item>;
    }
    export class Value {
        public Ref: string;
    }
    export class Content {
        public Items: Array<Item>;
    }
    export class ImportNode {
        public Config: Config;
        public DataTypes: DataTypes;
        public DocumentTypes: DocumentTypes;
        public Content: Content;
    }

}