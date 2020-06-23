import React, { Component } from 'react';
import { Modal as ReactstrapModal, Button, ModalHeader, ModalBody, ModalFooter } from "reactstrap";


export class Modal extends Component {
    static displayName = Modal.name;

    constructor(props) {
        super(props);

    }

    render() {
        return (
            <ReactstrapModal isOpen>
                <ModalHeader>
                    {this.props.title}
                </ModalHeader>

                <ModalBody>
                    {this.props.children}
                </ModalBody>

                <ModalFooter>
                    <Button onClick={this.props.cancel} color="secondary">{this.props.cancelButton}</Button>
                    <Button color="primary" onClick={this.props.success}>{this.props.successButton}</Button>
                </ModalFooter>
            </ReactstrapModal>);
    }
}