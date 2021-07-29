class Dialog extends HTMLElement {

    connectedCallback() {
        const shadowRoot = this.attachShadow({ mode: 'closed' });

    }
}

customElements.define("custom-dialog", Dialog);