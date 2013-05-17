amplify.request.define('productList', 'ajax', {
    url: 'http://localhost:63185/admin/api/product',
    dataType: 'jsonp',
    type: 'GET'
});
var product =
    {
        //view models
        productVariantViewModel: function (data) {

            this.name = data.name;
            this.price = data.price;
            this.active = data.active;
            this.requiresShipping = data.requiresShipping;
        },
        productVariantViewModelMapping:
        {
            create: function (options) {
                var r = ko.mapping.fromJS(new product.productVariantViewModel(options.data));//ko.mapping.fromJS(new product.productVariantViewModel(options.data));
                return r;
            }
        },
        productModel: function (data) {
            this.id = data.id;
            this.name = data.name;
            this.itemId = data.id.replace("products/", "");
            this.productVariants = data.productVariants;
        },
        viewModelMapping: {
            'products': {
                create: function (options) {
                    return ko.mapping.fromJS(new product.productModel(options.data));
                },
                update: function (options) {
                    var r = ko.mapping.fromJS(new product.productModel(options.data));
                    return r;
                }
            }
        },
        //events
        events: {
            productListReceived: 'productListReceived'
        },
        //event handlers
        onProductListReceived: function (options) {
            //product.viewModel.products(options);
            product.viewModel = ko.mapping.fromJS(options, product.viewModelMapping);
            ko.applyBindings(product.viewModel, document.getElementById('#products-list'));
            $('#product-list .collapse').on('show', function (item) {
                //product.onProductSelected(item);    
            });
        },
        onProductSelected: function (options) {
            var itemData = ko.dataFor(document.getElementById($(options.target).data('detail')));
            ko.renderTemplate('product-detail-template', itemData, {}, document.getElementById($(options.target).data('detail')));


        },
        init: function () {
            product.viewModel = ko.mapping.fromJS(product.viewModel, product.viewModelMapping);
            amplify.subscribe(product.events.productListReceived, function (data, text, jqXHR) {
                product.onProductListReceived(data);
            })
            amplify.request('productList', function (data, text, jqXHR) {
                amplify.publish(product.events.productListReceived, data);
            });

        }
    }
$(function () {
    product.init();
})