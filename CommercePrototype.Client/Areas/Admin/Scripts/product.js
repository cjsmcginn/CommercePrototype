amplify.request.define('productList', 'ajax', {
    url: 'http://localhost:63185/admin/api/product',
    dataType: 'jsonp',
    type: 'GET'
});
amplify.request.define('productSave', 'ajax', {
    url: '/api/proxy',
    dataType: 'json',
    type: 'PUT'
});
amplify.request.define('createProductSave', 'ajax', {
    url: 'http://localhost:63185/admin/api/product',
    dataType: 'json',
    type: 'POST'
});

var product =
    {
        //view model
        productMapping: {
            
            create: function (options) {
                var result = ko.mapping.fromJS({
                    name: ko.protectedObservable(options.data.name),
                    id: options.data.id,
                    active: ko.protectedObservable(options.data.active),
                    itemId: options.data.id.replace('products/', ''),
                    productVariants: options.data.productVariants,
                    createdOnUtc:options.data.createdOnUtc
                }, {
                    'productVariants': {
                        key: function (item) {
                            return ko.utils.unwrapObservable(item.name);
                        },
                        create: function (options) {
                            var result =  ko.mapping.fromJS({
                                name: ko.protectedObservable(options.data.name),
                                price: ko.protectedObservable(options.data.price),
                                requiresShipping: ko.protectedObservable(options.data.requiresShipping),
                                active: ko.protectedObservable(options.data.active),
                                id: '_' + Math.random().toString(36).substr(2, 9),
                                createdOnUtc:options.data.createdOnUtc                               
                            });
                            result.commitAll= function () {
                                this.name.commit();
                                this.price.commit();
                                this.requiresShipping.commit();
                                this.active.commit();

                            },
                            result.resetAll= function () {
                                this.name.reset();
                                this.price.reset();
                                this.requiresShipping.reset();
                                this.active.reset();
                            }  
                            return result;
                        }
                    }
                });
                result.edit= function (item) {
                    amplify.publish(product.events.productEdit, item);
                };
                result.cancel= function (item) {
                    amplify.publish(product.events.productEditCancel, item);
                };
                result.save= function (item) {
                    amplify.publish(product.events.productSave, item);
                };
                result.update = function (item) {
                    this.active(item.active);
                    this.name(item.name);
                };
                result.commitAll= function () {
                    this.name.commit();
                    this.active.commit();

                };
                result.resetAll= function () {
                    this.name.reset();
                    this.active.reset();
                };
                return result;
            }
        },
        
        mapping: {
            create:function(options){
                var result = ko.mapping.fromJS(options.data, {
                    'products': {
                        key: function (item) {
                            return ko.utils.unwrapObservable(item.itemId);
                        },
                        create: function (options) {
                            var result = ko.mapping.fromJS(options.data, product.productMapping);
                            return result;
                        }

                    }
                });
                result.addProduct = function () {
                    var options = {
                        data: {
                            id: 'products/0',
                            name: 'New Product',
                            active: true,
                            productVariants: []

                        }
                    };
                    var item = ko.mapping.fromJS(options.data, product.productMapping);
                    item.save = function (options) {
                        amplify.publish(product.events.createProductSave, options);
                    };
                    this.products.unshift(item);
                    amplify.publish(product.events.createProduct, item);
                };
                return result;
            }
            
        },
        currentProduct: null,
        viewModel: null,
        //events
        events: {
            productListReceived: 'productListReceived',
            productEdit: 'productEdit',
            productEditCancel: 'productEditCancel',
            productSave: 'productSave',
            productSaveComplete: 'productSaveComplete',
            createProduct: 'createProduct',
            createProductSave: 'createProductSave',
            createProductCancel: 'createProductCancel',
            createProductSaveComplete: 'createProductSaveComplete'
        },
        //event handlers
        onProductListReceived: function (options) {
            product.viewModel = ko.mapping.fromJS(options, product.mapping);
            ko.applyBindings(product.viewModel);
        },
        onProductEdit: function (options) {
            product.currentProduct = ko.mapping.toJS(options);
        },
        onProductSave: function (options) {
            options.commitAll();
            $('#product-save-modal').modal({ show: true });
            
            var item = ko.mapping.toJS(options);
            amplify.request('productSave', { uri: 'http://localhost:63185/admin/api/product', content: JSON.stringify(item) }, function (data,status,jqXHR) {
                console.log(data);
                amplify.publish(product.events.productSaveComplete, data);
            });
        },
        onProductSaveComplete: function (options) {
            $('#product-save-modal').modal('hide');
            var index = product.viewModel.products.mappedIndexOf({ itemId: options.id.replace('products/', '') });
            var existing = product.viewModel.products()[index];            
            existing.update(options);
            $('#product-list #' + existing.itemId() + '_detail').collapse('hide');
        },
        onProductEditCancel: function (options) {
            options.resetAll();
        },
        onCreateProduct: function (options) {
            var item = $('#' + product.viewModel.products()[0].itemId() + '_detail');
            item.collapse('show');

        },
        onCreateProductSave: function (options) {
            options.commitAll();
            $('#product-save-modal').modal({ show: true });
            var item = ko.mapping.toJS(options);
            amplify.request('createProductSave', item , function (data) {

                console.log('done');
            });
            //TODO: Implement put callback
            //window.setInterval(function () {
            //    amplify.publish(product.events.productSaveComplete, options);

            //}, 3000);
            var x = 'Y';
        },
        onCreateProductCancel: function (options) { },
        onCreateProductSaveComplete: function (options) { },
        init: function () {

            amplify.subscribe(product.events.productListReceived, function (data, text, jqXHR) {
                product.onProductListReceived(data);
            });
            amplify.subscribe(product.events.productSave, function (options) {
                product.onProductSave(options);
            });
            amplify.subscribe(product.events.productSaveComplete, function (options) {
                product.onProductSaveComplete(options);
            });
            amplify.subscribe(product.events.productEditCancel, function (options) {
                product.onProductEditCancel(options);
            });
            amplify.subscribe(product.events.productEdit, function (options) {
                product.onProductEdit(options);
            });
            amplify.subscribe(product.events.createProduct, function (options) {
                product.onCreateProduct(options);
            });
            amplify.subscribe(product.events.createProductSave, function (options) {
                product.onCreateProductSave(options);
            });
            amplify.request('productList', function (data, text, jqXHR) {
                amplify.publish(product.events.productListReceived, data);
            });

        }
    }
$(function () {
    product.init();
})