amplify.request.define('productList', 'ajax', {
    url: 'http://localhost:63185/admin/api/product',
    dataType: 'jsonp',
    type: 'GET'
});
amplify.request.define('productSave', 'ajax', {
    url: 'http://localhost:63185/admin/api/product',
    dataType: 'json',   
    type: 'PUT'
});
var product =
    {
        //view model
       mapping:{
            'products':{
                key:function(item) {        
                    return ko.utils.unwrapObservable(item.itemId);
                },
                create:function(options)
                {
                    var result = ko.mapping.fromJS({
                        name:ko.protectedObservable(options.data.name),
                        id:options.data.id,
                        active: ko.protectedObservable(options.data.active),
                        itemId:options.data.id.replace('products/',''),
                        productVariants: options.data.productVariants,
                        edit: function (item) {
                            amplify.publish(product.events.productEdit, item);
                        },
                        cancel: function (item) {
                            amplify.publish(product.events.productEditCancel, item);
                        },
                        save: function (item) {
                            amplify.publish(product.events.productSave, item);
                        },
                        commitAll: function () {
                            this.name.commit();
                            this.active.commit();
                            
                        },
                        resetAll: function () {
                            this.name.reset();
                            this.active.reset();
                        }

                    },{
                        'productVariants':{
                            key:function(item){
                                return ko.utils.unwrapObservable(item.name);
                            },
                            create: function (options) {
                                return ko.mapping.fromJS({
                                    name: ko.protectedObservable(options.data.name),
                                    price: ko.protectedObservable(options.data.price),
                                    requiresShipping: ko.protectedObservable(options.data.requiresShipping),
                                    active: ko.protectedObservable(options.data.active),
                                    id: '_' + Math.random().toString(36).substr(2, 9),
                                    commitAll: function () {
                                        this.name.commit();
                                        this.price.commit();
                                        this.requiresShipping.commit();
                                        this.active.commit();

                                    },
                                    resetAll: function () {
                                        this.name.reset();
                                        this.price.reset();
                                        this.requiresShipping.reset();
                                        this.active.reset();
                                    }
                                });
                            }
                        }
                    });
                    
                    return result;
                }
            }
       },
        currentProduct:null,
        viewModel:null,
        //events
        events: {
            productListReceived: 'productListReceived',
            productEdit: 'productEdit',
            productEditCancel: 'productEditCancel',
            productSave: 'productSave',
            productSaveComplete:'productSaveComplete'
        },
        //event handlers
        onProductListReceived: function (options) {
            product.viewModel = ko.mapping.fromJS(options, product.mapping);
            ko.applyBindings(product.viewModel);
        },
        onProductEdit:function(options){            
            product.currentProduct = ko.mapping.toJS(options);
        },
        onProductSave: function (options) {
            //var item = ko.mapping.toJS(options);
            // amplify.request('productSave',"stuff", function (data, text, jqXHR) {
            options.commitAll();
            $('#product-save-modal').modal({show:true});
            window.setInterval(function () {
                amplify.publish(product.events.productSaveComplete, options);
               
            }, 3000);
           
        },
        onProductSaveComplete: function (options) {
            $('#product-save-modal').modal('hide');
            $('#product-list #' + options.itemId() + '_detail').collapse('hide');
        },
        onProductEditCancel: function (options) {
            options.resetAll();    
        },
        init: function () {
            // product.viewModel = ko.mapping.fromJS(product.viewModel, product.viewModelMapping);

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
            amplify.request('productList', function (data, text, jqXHR) {
                amplify.publish(product.events.productListReceived, data);
            });

        }
    }
$(function () {
    product.init();
})