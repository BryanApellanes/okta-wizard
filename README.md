# Okta Visual Studio Template Wizard

## Description
The **Okta Visual Studio Template Wizard** helps a developer configure their Okta project by injecting Okta configuration into a project template resulting in a fully functional application configured for their organization, prior to writing any code.

## Installation
To install the Okta Visual Studio Template Wizard perform the following steps:

1. Start **Visual Studio**.

2. Select **Extensions** then **Manage Extensions**.

3. In the left pane select **Online**.

4. In the top right corner, enter `Okta` into the search box.
    
    `Okta Visual Studio Template Wizard` is displayed in the search results area.

5. Select `Okta Visual Studio Template Wizard` then click the `Download` button.

6. Close the **Manage Extensions** window.

7. Close **Visual Studio**.

    This should begin the VSIX installer.

## Verify Installation
To verify that you have the  Okta Visual Studio Template Wizard installed perform the following steps:

1. Start **Visual Studio**.
2. Select **Extensions** then **Manage Extensions**.
3. In the left pane select **Installed**.
    You should see **Okta Visual Studio Template Wizard** in the list of installed extensions.

## How it Works
The installation of **Okta Visual Studio Template Wizard** includes templates for .Net applications.  When an Okta template is selected as the basis of a new project, the Okta Visual Studio Template Wizard executes to configure the application with Okta integration built in.

## Create A New Project
The **Okta Visual Studio Template Wizard** executes when you select an Okta template from the available templates provided by **File → New → Project**.  Do the following to execute the **Okta Visual Studio Template Wizard**:

1. Start a new project. To start a new project do one of the following:
    
    a. Start **Visual Studio** and select `Create a new project`, or
    b. If **Visual Studio** is already running, select **File → New → Project** then select `Create a new project`.

2. In the `Create a new project` window type `Okta` into the search box.

3. Select the appropriate Okta template for your project and click `next`.

4. Complete the `Configure your new project` form by entering a `Project name`, `Location` and `Solution name` then click `Create`. 

5. Complete the `Configure Okta Api` form by entering your Okta Domain, Api Token and click OK.  

    > Note: This is the first screen rendered by the Okta Visual Studio Template Wizard.

6. Okta Visual Studio Template Wizard registers your new application for you, it acquires a Client Id and Client Secret using the Okta api credentials you provided in the previous step. 

    > Note: If an Okta application already exists by the name you specified you are prompted to manually enter the Client Secret. 

7. After the project is initialized Okta Visual Studio Template Wizard creates a test user to verify that Okta integration functions as expected.

    > Note: If an Okta application already exists by the name you specified a test user is not created.

8. Build and run your new project then login using the test credentials created by the **Okta Visual Studio Template Wizard**.