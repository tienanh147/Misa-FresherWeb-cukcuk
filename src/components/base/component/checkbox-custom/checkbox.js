import { arrayJsonPush, arrayJsonDelete } from '../../js/base/commonFunction.js'

class Check extends HTMLElement {
    connectedCallback() {

        // const linkCss = document.createElement('link');
        // linkCss.rel = 'stylesheet';
        // linkCss.href = this.getAttribute('url') + 'checkbox.css';



        // const linkFontAwesome = document.createElement('link');
        // linkFontAwesome.rel = 'stylesheet';
        // linkFontAwesome.href = 'https://pro.fontawesome.com/releases/v5.10.0/css/all.css';
        // const checkbox = document.createElement('div');
        // checkbox.className = 'checkbox-custom';

        // const shadowRoot = this.attachShadow({ mode: 'closed' });
        // shadowRoot.appendChild(linkCss);
        // shadowRoot.appendChild(linkFontAwesome);
        // shadowRoot.appendChild(checkbox);

        this.className = 'checkbox-custom';
        const iconChecked = document.createElement('i');
        iconChecked.className = "far fa-check";

        this.appendChild(iconChecked);
        $(this).click(function(e) {
            e.stopPropagation();
            $(this).toggleClass("checked");

            //set extra event: tô màu cho hàng, hiển thị nút delete và thêm vào deleteList
            try {
                this.check('EmployeeId', 'EmployeeCode');
                $(this).parent('td').parents('tr').toggleClass('checked');
            } catch (error) {

            }
        });

        $(this).dblclick(function(e) {
            e.stopPropagation();
        });

    }


    /**
     * Hàm check cho checkbox hiển thị nút delete và chỉnh sửa deleteList
     * @param {String} idFieldName tên trường id của bảng(VD: EmployeeId)
     * @param {String} codeFieldName tên trường code của bảng(VD: EmployeeCode)
     * 
     * CreatedBy: TienAnh(27/07/2021)
     */
    check(idFieldName, codeFieldName) {

        var tr = $(this).parent().parent();
        var id = tr.attr(idFieldName);
        var code = tr.find(`td[fieldname='${codeFieldName}']`).text();

        var obj = {
            'code': code,
            'id': id
        }
        if ($(this).hasClass("checked")) {
            $('#buttonDel').css('visibility', 'visible');
            sessionStorage['deleteList'] = arrayJsonPush(obj, sessionStorage['deleteList']);
        } else {

            sessionStorage['deleteList'] = arrayJsonDelete(sessionStorage['deleteList'], function(item) {
                return item['id'] == obj['id'];
            });
            if (sessionStorage['deleteList'] == '[]') {
                $('#buttonDel').css('visibility', 'hidden');
            }
        }
    }
}
customElements.define('custom-checkbox', Check);