# LojaGeek E-Commerce
The project was created using the ASP.Net MVC5, Razor, and Bootstrap tools. Built to serve as an e-commerce platform for the sale of physical video games, it aims to be a concise, comprehensive website with current development trends, such as responsiveness and user experience (UX).

## Getting Started
Open the .sln file at the root of the project with Visual Studio. After loading all configurations, press F5 to create a local server and open the site in the browser.

### Prerequisites
- Windows operating system.
- Visual Studio, preferably version 2017.

## System Description
### General
- The site has an aesthetic based on the Bootstrap library.
- It functions similarly to a typical e-commerce site.
- A visitor can browse the storefront, create a shopping cart, view product details, comment on a product, with the only limitation being the purchase.
- An administrator can log in, perform all actions a customer can, except for making purchases. Additionally, administrators can control inventory, coupons, and purchases through dedicated pages for these functions.
- The database is modeled with the Entity-Relationship Diagram (ERD) shown below:

![ERD](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/DER.PNG) **System ERD under development**

#### General System Improvements
- [x] Improved application of Bootstrap.
- [x] Enhanced responsiveness.
- [ ] Creation of a customer profile page.
- [x] Creation of a page for the customer to enter an address.
- [ ] Creation of a comment administration tool for an administrator.

### Home Page
- The home page features a fixed top menu with the store name and some menu items.
- The store name is clickable and leads to the home page, regardless of the user's location in the system.
- Analogous to the side is a button with a house-shaped symbol that performs the same action as the logo.
- The register button leads to a form for registering a new customer.
- The login button takes the customer to a login form.
- The shopping cart symbol leads to the cart page.
- Below is the list of games if any have been added.
- Under all products are two buttons, details, and one with a shopping cart glyphicon.
- The details button takes you to the product detail page.
- The button with a cart adds the product to the cart.
- The game listing starts from the center and spreads the games equally.
- The footer below contains a copyright link to the administrative area.
- Clicking the link redirects to the administrative login page.

#### Home Page Improvements
- [x] More concise product listing.

![Home Page without a logged-in customer](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Inicial.png) **Home Page without a logged-in customer**

![Home Page with a logged-in customer](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Inicial%20com%20cliente%20logado.png) **Home Page with a logged-in customer**

### Administrative Login Page
- The menu remains the same as the home page.
- There is a centered form with only one input, one label, and one button.
- The administrative password is an automatically generated password taking into account dates and times.
- If the password is incorrect, the system returns to the initial screen.
- If the password is correct, the user is taken to the inventory management screen.

![Administrative Login Page](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Login%20aadministrativo.png) **Administrative Login Page**

### Inventory Page
- The menu is now the administrative one, containing options to log out and navigate to the inventory page, in addition to the options of the logo and home symbol mentioned in the home page.
- There is a New Product button that leads to a form page for adding a new product.
- Below is a table with the product title, a row of titles containing a photo, name, price, quantity in stock, active status, and actions.
- In the photo column, a small thumbnail of the associated product photo is loaded.
- In the name column, the product name is displayed.
- In the price column, the current price of the product is displayed.
- In the quantity in stock column, the current quantity of the product is displayed.
- In the active column, it is shown whether the product is active and visible for purchase or not.
- In the actions column, action link buttons for the product are loaded, such as "Deactivate Product," "Activate Product," and "Add to Inventory."

#### Inventory Page Improvements
- [x] Fix the crossed-out nominations and appearances above for usability.
- [x] Implement loading the crossed-out thumbnail photo.
- [x] Prevent activation of the product if the stock is zero.
- [x] Prevent access to administrative pages through direct links.
- [x] Create an area for creating coupons.

![Inventory Page](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Controle%20de%20estoque.png) **Inventory Page**

### New Product Registration Page
- A form with a title to register a product.
- The form contains name, description, platform, stock, price, photo, an active checkbox, and a button to save the product.
- The price input has a placeholder explaining what should be entered.
- All these inputs are information to store a product in the database.
- There is a button at the beginning of the form to return to the inventory listing.
- When saving, the price must be automatically calculated, taking into account all costs and taxes behind it.
- After saving, return to the product listing on the inventory view.

#### New Product Registration Page Improvements
- [x] Make the photo input an input file for uploading a thumbnail image.
- [x] Make the platform input a dropdown list.
- [x] Fix the crossed-out link above.
- [ ] Modify the model to add game style.

![New Product Registration Page]![Página Cadastro de Novo Produto](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Cadastro%20produto.png) **New Product Registration Page**

### Add Inventory Page
- A form with a title to Add Inventory.
- The form contains a product name, description, filled with the product to be added to inventory.
- Editable stock and price inputs for the price of the new pieces and their quantity, along with a button to add inventory.
- There is a link below the form to return to the inventory screen.
- When adding to inventory, the real price will be calculated, and an average of the prices for the product's new price will be calculated automatically.

#### Add Inventory Page Improvements
- [ ] Zero out the values in the stock and price inputs to improve usability.
- [ ] Prevent input of negative numbers.

![Add Inventory Page](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Repor%20estoque.png) **Add Inventory Page**

### Customer Registration Page
- A form with a title for Customer Registration.
- The form contains name, surname, CPF, email, password, and interests checkbox.
- Below the input is a button to register.
- After submitting, the customer is recorded in the database.
- After

 the customer is saved, they are taken to the login screen.

#### Customer Registration Page Improvements
- [ ] Mask for CPF.
- [ ] Specific input for email.
- [ ] Input to confirm the password.
- [x] Improve interests.

![Customer Registration Page](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Cadastro%20cliente.png) **Customer Registration Page**

### Customer Login Page
- It has a form titled login.
- Input for email and password.
- There is a Send button.
- When the form is filled out and sent, it is tested in the database if it exists.
- If the user exists, they are logged into the session, and the menu changes to a customer menu with their name as an item, a logout button, a shopping cart logo, and a home symbol.
- If the user does not exist, they are sent back to the home page.

#### Customer Login Page Improvements
- [x] Error message in case of unsuccessful login.
- [x] Link to register customer.

![Customer Login Page](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Login%20cleinte.png) **Customer Login Page**

### Product Detail Page
- The screen displays all product information except whether it is active or not, as this is exclusive to administration.
- It has a textarea to enter a comment.
- It has an input to enter the name of the person who will comment.
- A button to send the comment.
- A button to add the product to the cart.
- At the bottom of the page is a list of product comments.

#### Product Detail Page Improvements
- [x] Fix send to cart button.
- [x] Approximate details to other e-commerce sites.

![Product Detail Page](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Detalhes%20do%20produto.png) **Product Detail Page**

### Cart Page
- A table titled cart.
- A column with the product header, which has the product name.
- A column with the price header, which has the price of the product times the quantity.
- A column with the quantity header, which has the current quantity of the product and a select with the maximum number of the product's stock, an update total button that, when pressed, updates the quantity and prices.
- A column with the ~~actions~~ header, which has the "Product Details" button, which takes you to the product detail, and the button to remove the product from the cart.
- An area for the Total Value of the cart.
- An area to apply a coupon, with an error message if it is not possible to apply the coupon.
- An area to apply shipping, with a message of how much the shipping is.
- At the bottom, if there is no logged-in user, a register button and a login button, which takes you to the customer registration view and the login view, respectively.
- At the bottom, if there is a logged-in user, a buy button that takes you to the purchase confirmation view.

#### Cart Page Improvements
- [x] Improve error and success messages.
- [ ] Create an empty cart view.

![Cart Page without a logged-in customer](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Carrinho%20deslogado.png) **Cart Page without a logged-in customer**

![Cart Page with a logged-in customer](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Carrinho%20logado.png) **Cart Page with a logged-in customer**

### Purchase Page
- A static page with text stating the name, CPF, purchase date, items purchased with their quantities and prices, total purchase value, and final purchase instructions.

#### Purchase Page Improvements
- [x] Add an intermediary view for address registration.
- [x] Create an invoice-like appearance.

![Purchase Page](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Comprovante%20de%20compra.png) **Purchase Page**

## Built with Tools
* [Asp.Net MVC 5](https://docs.microsoft.com/pt-br/aspnet/mvc/overview/getting-started/introduction/getting-started) - The backend framework used.
* [Razor](http://jakeydocs.readthedocs.io/en/latest/mvc/views/razor.html) - The web framework used.
* [Bootstrap](https://getbootstrap.com/docs/4.1/getting-started/introduction/) - CSS Framework used.
* [JQuery](https://api.jquery.com/) - JavaScript framework.
* [NHibernate](http://hibernate.org/orm/documentation/5.2/) - Database framework used.

## Authors
* **Gabriel Ramos de Sousa** - *Current project maintainer, System maintenance, refactoring, and ongoing development* - [GabrielDSousa](https://github.com/GabrielDSousa)
* **Danilo Itagyba** - *Initiated the project* - [itagyba](https://github.com/itagyba)
* **João Paulo** - *Assisted in documentation* - [JoaoPaulo333](https://github.com/JoaoPaulo333)

## License
This project is licensed under the MIT License.

## Acknowledgments
* To Professor Tadeu, for guiding the project with incremental and weekly requirements.
