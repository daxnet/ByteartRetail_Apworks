<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="2c3a9999-b8af-415a-88d3-c3a14da22fa8" namespace="ByteartRetail.Infrastructure.Config" xmlSchemaNamespace="urn:ByteartRetail.Infrastructure.Config" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="ByteartRetailConfigSection" namespace="ByteartRetail.Infrastructure.Config" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="byteartRetailConfigSection">
      <elementProperties>
        <elementProperty name="PermissionKeys" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="permissionKeys" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/2c3a9999-b8af-415a-88d3-c3a14da22fa8/PermissionKeyElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="Presentation" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="presentation" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/2c3a9999-b8af-415a-88d3-c3a14da22fa8/PresentationElement" />
          </type>
        </elementProperty>
        <elementProperty name="EmailClient" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="emailClient" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/2c3a9999-b8af-415a-88d3-c3a14da22fa8/EmailClientElement" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="PermissionKeyElementCollection" namespace="ByteartRetail.Infrastructure.Config" xmlItemName="permissionKey" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/2c3a9999-b8af-415a-88d3-c3a14da22fa8/PermissionKeyElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="PermissionKeyElement" namespace="ByteartRetail.Infrastructure.Config">
      <attributeProperties>
        <attributeProperty name="RoleName" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="roleName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/2c3a9999-b8af-415a-88d3-c3a14da22fa8/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="KeyName" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="keyName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/2c3a9999-b8af-415a-88d3-c3a14da22fa8/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="PresentationElement" namespace="ByteartRetail.Infrastructure.Config">
      <attributeProperties>
        <attributeProperty name="ProductsPageSize" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="productsPageSize" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/2c3a9999-b8af-415a-88d3-c3a14da22fa8/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="EmailClientElement" namespace="ByteartRetail.Infrastructure.Config">
      <attributeProperties>
        <attributeProperty name="Host" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="host" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/2c3a9999-b8af-415a-88d3-c3a14da22fa8/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Port" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="port" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/2c3a9999-b8af-415a-88d3-c3a14da22fa8/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="UserName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="userName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/2c3a9999-b8af-415a-88d3-c3a14da22fa8/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Password" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="password" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/2c3a9999-b8af-415a-88d3-c3a14da22fa8/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="EnableSsl" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="enableSsl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/2c3a9999-b8af-415a-88d3-c3a14da22fa8/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="Sender" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="sender" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/2c3a9999-b8af-415a-88d3-c3a14da22fa8/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>