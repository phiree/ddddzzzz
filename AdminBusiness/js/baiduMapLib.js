/**
 * ����baidu��
 */
var baidu = baidu || {guid : "$BAIDU$"};
(function() {
    // һЩҳ�漶��Ψһ�����ԣ���Ҫ������window[baidu.guid]��
    window[baidu.guid] = {};

    /**
     * ��Դ������������Կ�����Ŀ�������
     * @name baidu.extend
     * @function
     * @grammar baidu.extend(target, source)
     * @param {Object} target Ŀ�����
     * @param {Object} source Դ����
     * @returns {Object} Ŀ�����
     */
    baidu.extend = function (target, source) {
        for (var p in source) {
            if (source.hasOwnProperty(p)) {
                target[p] = source[p];
            }
        }
        return target;
    };

    /**
     * @ignore
     * @namespace
     * @baidu.lang �����Բ���ķ�װ�����������жϡ�ģ����չ���̳л����Լ������Զ����¼���֧�֡�
     * @property guid �����Ψһ��ʶ
     */
    baidu.lang = baidu.lang || {};

    /**
     * ����һ����ǰҳ���Ψһ��ʶ�ַ�����
     * @function
     * @grammar baidu.lang.guid()
     * @returns {String} ��ǰҳ���Ψһ��ʶ�ַ���
     */
    baidu.lang.guid = function() {
        return "TANGRAM__" + (window[baidu.guid]._counter ++).toString(36);
    };

    window[baidu.guid]._counter = window[baidu.guid]._counter || 1;

    /**
     * �������ʵ��������
     * keyΪÿ��ʵ����guid
     */
    window[baidu.guid]._instances = window[baidu.guid]._instances || {};

    /**
     * Tangram�̳л����ṩ��һ�����࣬�û�����ͨ���̳�baidu.lang.Class����ȡ�������Լ�������
     * @function
     * @name baidu.lang.Class
     * @grammar baidu.lang.Class(guid)
     * @param {string} guid �����Ψһ��ʶ
     * @meta standard
     * @remark baidu.lang.Class�����������ʵ��������һ��ȫ��Ψһ�ı�ʶguid��
     * guid���ڹ��캯�������ɵģ���ˣ��̳���baidu.lang.Class����Ӧ��ֱ�ӻ��߼�ӵ������Ĺ��캯����<br>
     * baidu.lang.Class�Ĺ��캯���в���guid�ķ�ʽ���Ա�֤guid��Ψһ�ԣ���ÿ��ʵ������һ��ȫ��Ψһ��guid��
     */
    baidu.lang.Class = function(guid) {
        this.guid = guid || baidu.lang.guid();
        window[baidu.guid]._instances[this.guid] = this;
    };

    window[baidu.guid]._instances = window[baidu.guid]._instances || {};

    /**
     * �ж�Ŀ������Ƿ�string���ͻ�String����
     * @name baidu.lang.isString
     * @function
     * @grammar baidu.lang.isString(source)
     * @param {Any} source Ŀ�����
     * @shortcut isString
     * @meta standard
     *
     * @returns {boolean} �����жϽ��
     */
    baidu.lang.isString = function (source) {
        return '[object String]' == Object.prototype.toString.call(source);
    };

    /**
     * �ж�Ŀ������Ƿ�Ϊfunction��Functionʵ��
     * @name baidu.lang.isFunction
     * @function
     * @grammar baidu.lang.isFunction(source)
     * @param {Any} source Ŀ�����
     * @returns {boolean} �����жϽ��
     */
    baidu.lang.isFunction = function (source) {
        return '[object Function]' == Object.prototype.toString.call(source);
    };

    /**
     * ������Ĭ�ϵ�toString������ʹ�÷�����Ϣ����׼ȷһЩ��
     * @return {string} �����String��ʾ��ʽ
     */
    baidu.lang.Class.prototype.toString = function(){
        return "[object " + (this._className || "Object" ) + "]";
    };

    /**
     * �ͷŶ��������е���Դ����Ҫ���Զ����¼���
     * @name dispose
     * @grammar obj.dispose()
     */
    baidu.lang.Class.prototype.dispose = function(){
        delete window[baidu.guid]._instances[this.guid];
        for(var property in this){
            if (!baidu.lang.isFunction(this[property])) {
                delete this[property];
            }
        }
        this.disposed = true;
    };

    /**
     * �Զ�����¼�����
     * @function
     * @name baidu.lang.Event
     * @grammar baidu.lang.Event(type[, target])
     * @param {string} type  �¼��������ơ�Ϊ�˷��������¼���һ����ͨ�ķ������¼��������Ʊ�����"on"(Сд)��ͷ��
     * @param {Object} [target]�����¼��Ķ���
     * @meta standard
     * @remark �����ģ�飬���Զ�ΪClass����3���¼���չ������addEventListener��removeEventListener��dispatchEvent��
     * @see baidu.lang.Class
     */
    baidu.lang.Event = function (type, target) {
        this.type = type;
        this.returnValue = true;
        this.target = target || null;
        this.currentTarget = null;
    };

    /**
     * ע�������¼�������������baidu.lang.Event��Class������ʵ���Ż��ø÷�����
     * @grammar obj.addEventListener(type, handler[, key])
     * @param   {string}   type         �Զ����¼�������
     * @param   {Function} handler      �Զ����¼�������ʱӦ�õ��õĻص�����
     * @param   {string}   [key]        Ϊ�¼���������ָ�������ƣ������Ƴ�ʱʹ�á�������ṩ��������Ĭ��Ϊ������һ��ȫ��Ψһ��key��
     * @remark  �¼��������ִ�Сд������Զ����¼����Ʋ�����Сд"on"��ͷ���÷������������"on"�ٽ����жϣ���"click"��"onclick"�ᱻ��Ϊ��ͬһ���¼���
     */
    baidu.lang.Class.prototype.addEventListener = function (type, handler, key) {
        if (!baidu.lang.isFunction(handler)) {
            return;
        }
        !this.__listeners && (this.__listeners = {});
        var t = this.__listeners, id;
        if (typeof key == "string" && key) {
            if (/[^\w\-]/.test(key)) {
                throw("nonstandard key:" + key);
            } else {
                handler.hashCode = key;
                id = key;
            }
        }
        type.indexOf("on") != 0 && (type = "on" + type);
        typeof t[type] != "object" && (t[type] = {});
        id = id || baidu.lang.guid();
        handler.hashCode = id;
        t[type][id] = handler;
    };

    /**
     * �Ƴ�������¼�������������baidu.lang.Event��Class������ʵ���Ż��ø÷�����
     * @grammar obj.removeEventListener(type, handler)
     * @param {string}   type     �¼�����
     * @param {Function|string} handler  Ҫ�Ƴ����¼������������߼���������key
     * @remark  ����ڶ�������handlerû�б��󶨵���Ӧ���Զ����¼��У�ʲôҲ������
     */
    baidu.lang.Class.prototype.removeEventListener = function (type, handler) {
        if (baidu.lang.isFunction(handler)) {
            handler = handler.hashCode;
        } else if (!baidu.lang.isString(handler)) {
            return;
        }
        !this.__listeners && (this.__listeners = {});
        type.indexOf("on") != 0 && (type = "on" + type);
        var t = this.__listeners;
        if (!t[type]) {
            return;
        }
        t[type][handler] && delete t[type][handler];
    };

    /**
     * �ɷ��Զ����¼���ʹ�ð󶨵��Զ����¼�����ĺ������ᱻִ�С�����baidu.lang.Event��Class������ʵ���Ż��ø÷�����
     * @grammar obj.dispatchEvent(event, options)
     * @param {baidu.lang.Event|String} event   Event���󣬻��¼�����(1.1.1��֧��)
     * @param {Object} options ��չ����,�������Լ�ֵ����չ��Event������(1.2��֧��)
     * @remark ��������ͨ��addEventListenr�󶨵��Զ����¼��ص�����֮�⣬�������ֱ�Ӱ󶨵�����������Զ����¼���
     * ���磺<br>
     * myobj.onMyEvent = function(){}<br>
     * myobj.addEventListener("onMyEvent", function(){});
     */
    baidu.lang.Class.prototype.dispatchEvent = function (event, options) {
        if (baidu.lang.isString(event)) {
            event = new baidu.lang.Event(event);
        }
        !this.__listeners && (this.__listeners = {});
        options = options || {};
        for (var i in options) {
            event[i] = options[i];
        }
        var i, t = this.__listeners, p = event.type;
        event.target = event.target || this;
        event.currentTarget = this;
        p.indexOf("on") != 0 && (p = "on" + p);
        baidu.lang.isFunction(this[p]) && this[p].apply(this, arguments);
        if (typeof t[p] == "object") {
            for (i in t[p]) {
                t[p][i].apply(this, arguments);
            }
        }
        return event.returnValue;
    };

    /**
     * Ϊ���͹����������̳й�ϵ
     * @name baidu.lang.inherits
     * @function
     * @grammar baidu.lang.inherits(subClass, superClass[, className])
     * @param {Function} subClass ���๹����
     * @param {Function} superClass ���๹����
     * @param {string} className ������ʶ
     * @remark ʹsubClass�̳�superClass��prototype��
     * ���subClass��ʵ���ܹ�ʹ��superClass��prototype�ж�����������Ժͷ�����<br>
     * �������ʵ�����ǽ�����subClass��superClass��ԭ�������ɣ�����subClass������constructor������<br>
     * <strong>ע�⣺���Ҫ�̳й��캯������Ҫ��subClass����callһ�£�����������demo����</strong>
     * @shortcut inherits
     * @meta standard
     * @see baidu.lang.Class
     */
    baidu.lang.inherits = function (subClass, superClass, className) {
        var key, proto,
            selfProps = subClass.prototype,
            clazz = new Function();
        clazz.prototype = superClass.prototype;
        proto = subClass.prototype = new clazz();
        for (key in selfProps) {
            proto[key] = selfProps[key];
        }
        subClass.prototype.constructor = subClass;
        subClass.superClass = superClass.prototype;

        if ("string" == typeof className) {
            proto._className = className;
        }
    };

    /**
     * @ignore
     * @namespace baidu.dom ����dom�ķ�����
     */
    baidu.dom = baidu.dom || {};

    /**
     * ���ĵ��л�ȡָ����DOMԪ��
     *
     * @param {string|HTMLElement} id Ԫ�ص�id��DOMԪ��
     * @meta standard
     * @return {HTMLElement} DOMԪ�أ���������ڣ�����null������������Ϸ���ֱ�ӷ��ز���
     */
    baidu._g = baidu.dom._g = function (id) {
        if (baidu.lang.isString(id)) {
            return document.getElementById(id);
        }
        return id;
    };

    /**
     * ���ĵ��л�ȡָ����DOMԪ��
     * @name baidu.dom.g
     * @function
     * @grammar baidu.dom.g(id)
     * @param {string|HTMLElement} id Ԫ�ص�id��DOMԪ��
     * @meta standard
     *
     * @returns {HTMLElement|null} ��ȡ��Ԫ�أ����Ҳ���ʱ����null,����������Ϸ���ֱ�ӷ��ز���
     */
    baidu.g = baidu.dom.g = function (id) {
        if ('string' == typeof id || id instanceof String) {
            return document.getElementById(id);
        } else if (id && id.nodeName && (id.nodeType == 1 || id.nodeType == 9)) {
            return id;
        }
        return null;
    };

    /**
     * ��Ŀ��Ԫ�ص�ָ��λ�ò���HTML����
     * @name baidu.dom.insertHTML
     * @function
     * @grammar baidu.dom.insertHTML(element, position, html)
     * @param {HTMLElement|string} element Ŀ��Ԫ�ػ�Ŀ��Ԫ�ص�id
     * @param {string} position ����html��λ����Ϣ��ȡֵΪbeforeBegin,afterBegin,beforeEnd,afterEnd
     * @param {string} html Ҫ�����html
     * @remark
     *
     * ����position��������Сд������<br>
     * ��������˼��beforeBegin&lt;span&gt;afterBegin   this is span! beforeEnd&lt;/span&gt; afterEnd <br />
     * ���⣬���ʹ�ñ������������script��ǩ��HTML�ַ�����script��ǩ��Ӧ�Ľű������ᱻִ�С�
     *
     * @shortcut insertHTML
     * @meta standard
     *
     * @returns {HTMLElement} Ŀ��Ԫ��
     */
    baidu.insertHTML = baidu.dom.insertHTML = function (element, position, html) {
        element = baidu.dom.g(element);
        var range,begin;

        if (element.insertAdjacentHTML) {
            element.insertAdjacentHTML(position, html);
        } else {
            // ���ﲻ��"undefined" != typeof(HTMLElement) && !window.opera�жϣ������������������
            // ������ʵ�����жϣ�����������µ�����������Ͳ���ִ����
            range = element.ownerDocument.createRange();
            // FF��range��λ�����ô�����ܵ��´���������fragment�ڲ���dom��֮��html�ṹ�ҵ�
            // ����range.insertNode������html, by wenyuxiang @ 2010-12-14.
            position = position.toUpperCase();
            if (position == 'AFTERBEGIN' || position == 'BEFOREEND') {
                range.selectNodeContents(element);
                range.collapse(position == 'AFTERBEGIN');
            } else {
                begin = position == 'BEFOREBEGIN';
                range[begin ? 'setStartBefore' : 'setEndAfter'](element);
                range.collapse(begin);
            }
            range.insertNode(range.createContextualFragment(html));
        }
        return element;
    };

    /**
     * ΪĿ��Ԫ�����className
     * @name baidu.dom.addClass
     * @function
     * @grammar baidu.dom.addClass(element, className)
     * @param {HTMLElement|string} element Ŀ��Ԫ�ػ�Ŀ��Ԫ�ص�id
     * @param {string} className Ҫ��ӵ�className������ͬʱ��Ӷ��class���м�ʹ�ÿհ׷��ָ�
     * @remark
     * ʹ����Ӧ��֤�ṩ��className�Ϸ��ԣ���Ӧ�������Ϸ��ַ���className�Ϸ��ַ��ο���http://www.w3.org/TR/CSS2/syndata.html��
     * @shortcut addClass
     * @meta standard
     *
     * @returns {HTMLElement} Ŀ��Ԫ��
     */
    baidu.ac = baidu.dom.addClass = function (element, className) {
        element = baidu.dom.g(element);
        var classArray = className.split(/\s+/),
            result = element.className,
            classMatch = " " + result + " ",
            i = 0,
            l = classArray.length;

        for (; i < l; i++){
            if ( classMatch.indexOf( " " + classArray[i] + " " ) < 0 ) {
                result += (result ? ' ' : '') + classArray[i];
            }
        }

        element.className = result;
        return element;
    };

    /**
     * @ignore
     * @namespace baidu.event ��������������Ե��¼���װ��
     * @property target     �¼��Ĵ���Ԫ��
     * @property pageX      ����¼������x����
     * @property pageY      ����¼������y����
     * @property keyCode    �����¼��ļ�ֵ
     */
    baidu.event = baidu.event || {};

    /**
     * �¼��������Ĵ洢��
     * @private
     * @meta standard
     */
    baidu.event._listeners = baidu.event._listeners || [];

    /**
     * ΪĿ��Ԫ������¼�������
     * @name baidu.event.on
     * @function
     * @grammar baidu.event.on(element, type, listener)
     * @param {HTMLElement|string|window} element Ŀ��Ԫ�ػ�Ŀ��Ԫ��id
     * @param {string} type �¼�����
     * @param {Function} listener ��Ҫ��ӵļ�����
     * @remark
     *  1. ��֧�ֿ���������������¼����������<br>
     *  2. �ķ�����Ϊ�����������¼������Է�ֹ��iframe�¼����ص��¼������ȡʧ��
     * @shortcut on
     * @meta standard
     * @see baidu.event.un
     *
     * @returns {HTMLElement|window} Ŀ��Ԫ��
     */
    baidu.on = baidu.event.on = function (element, type, listener) {
        type = type.replace(/^on/i, '');
        element = baidu._g(element);
        var realListener = function (ev) {
                // 1. ���ﲻ֧��EventArgument,  ԭ���ǿ�frame���¼�����
                // 2. element��Ϊ������this
                listener.call(element, ev);
            },
            lis = baidu.event._listeners,
            filter = baidu.event._eventFilter,
            afterFilter,
            realType = type;
        type = type.toLowerCase();
        // filter����
        if(filter && filter[type]){
            afterFilter = filter[type](element, type, realListener);
            realType = afterFilter.type;
            realListener = afterFilter.listener;
        }
        // �¼�����������
        if (element.addEventListener) {
            element.addEventListener(realType, realListener, false);
        } else if (element.attachEvent) {
            element.attachEvent('on' + realType, realListener);
        }

        // ���������洢��������
        lis[lis.length] = [element, type, listener, realListener, realType];
        return element;
    };

    /**
     * ΪĿ��Ԫ���Ƴ��¼�������
     * @name baidu.event.un
     * @function
     * @grammar baidu.event.un(element, type, listener)
     * @param {HTMLElement|string|window} element Ŀ��Ԫ�ػ�Ŀ��Ԫ��id
     * @param {string} type �¼�����
     * @param {Function} listener ��Ҫ�Ƴ��ļ�����
     * @shortcut un
     * @meta standard
     *
     * @returns {HTMLElement|window} Ŀ��Ԫ��
     */
    baidu.un = baidu.event.un = function (element, type, listener) {
        element = baidu._g(element);
        type = type.replace(/^on/i, '').toLowerCase();

        var lis = baidu.event._listeners,
            len = lis.length,
            isRemoveAll = !listener,
            item,
            realType, realListener;

        //�����listener�Ľṹ�ĳ�json
        //���Խ�ʡ�����ѭ�����Ż�����
        //��������un��ʹ��Ƶ�ʲ����ߣ�ͬʱ��listener�����ʱ��
        //����������������Ĳ���Դ������Ӱ��
        //�ݲ����Ǵ��Ż�
        while (len--) {
            item = lis[len];

            // listener����ʱ���Ƴ�element��������listener������type�����¼�
            // listener������ʱ���Ƴ�element������type�����¼�
            if (item[1] === type
                && item[0] === element
                && (isRemoveAll || item[2] === listener)) {
                realType = item[4];
                realListener = item[3];
                if (element.removeEventListener) {
                    element.removeEventListener(realType, realListener, false);
                } else if (element.detachEvent) {
                    element.detachEvent('on' + realType, realListener);
                }
                lis.splice(len, 1);
            }
        }
        return element;
    };

    /**
     * ��ȡevent�¼�,�����ͬ�������������
     * @param {Event}
     * @return {Event}
     */
    baidu.getEvent = baidu.event.getEvent = function (event) {
        return window.event || event;
    }

    /**
     * ��ȡevent.target,�����ͬ�������������
     * @param {Event}
     * @return {Target}
     */
    baidu.getTarget = baidu.event.getTarget = function (event) {
        var event = baidu.getEvent(event);
        return event.target || event.srcElement;
    }

    /**
     * ��ֹ�¼���Ĭ����Ϊ
     * @name baidu.event.preventDefault
     * @function
     * @grammar baidu.event.preventDefault(event)
     * @param {Event} event �¼�����
     * @meta standard
     */
    baidu.preventDefault = baidu.event.preventDefault = function (event) {
        var event = baidu.getEvent(event);
        if (event.preventDefault) {
            event.preventDefault();
        } else {
            event.returnValue = false;
        }
    };

    /**
     * ֹͣ�¼�ð�ݴ���
     * @param {Event}
     */
    baidu.stopBubble = baidu.event.stopBubble = function (event) {
        event = baidu.getEvent(event);
        event.stopPropagation ? event.stopPropagation() : event.cancelBubble = true;
    }

    baidu.browser = baidu.browser || {};

    if (/msie (\d+\.\d)/i.test(navigator.userAgent)) {
        //IE 8�£���documentModeΪ׼
        //�ڰٶ�ģ���У����ܻ���$����ֹ��ͻ����$1 д�� \x241
        /**
         * �ж��Ƿ�Ϊie�����
         * @property ie ie�汾��
         * @grammar baidu.browser.ie
         * @meta standard
         * @shortcut ie
         * @see baidu.browser.firefox,baidu.browser.safari,baidu.browser.opera,baidu.browser.chrome,baidu.browser.maxthon
         */
        baidu.browser.ie = baidu.ie = document.documentMode || + RegExp['\x241'];
    }

})();