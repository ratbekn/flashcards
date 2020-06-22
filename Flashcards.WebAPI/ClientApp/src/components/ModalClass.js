import React, { Component } from 'react';
import { Modal, Button, ModalHeader, ModalBody, ModalFooter } from "reactstrap";


export class ModalClass extends Component {
    static displayName = ModalClass.name;

    constructor(props) {
        super(props);

    }

    render() {
        return (
            <Modal isOpen>
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
            </Modal>);
    }
}