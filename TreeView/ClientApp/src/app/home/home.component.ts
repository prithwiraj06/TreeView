import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ProjectTreeService } from '../services/project-tree.service';
import { Plan } from '../models/plan.model';
import { TreeNode } from 'primeng/api';
import { Lob } from '../models/lob.model';
import { Product } from '../models/product.model';
import { Domain } from '../models/domain.model';
import { Measure } from '../models/measure.model';
import { Tree } from 'primeng/tree';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class HomeComponent implements OnInit {

  isProjectTreeLoading: boolean = true;
  projectTreeData: TreeNode[] = [];
  selectedNode: TreeNode;
  selectedProduct: TreeNode;
  selectedMeasure: TreeNode;
  hideProjectTree: boolean = false;
  selectedNodeStyle: string = 'highlight-node';

  @ViewChild('projectTree') projectTree: Tree;

  constructor(private _projectTreeService: ProjectTreeService) { }

  ngOnInit() {
    this.getProjectTreeData();
  }

  getProjectTreeData() {
    this._projectTreeService.getProjectTree().subscribe((result: Plan[]) => {

      result.forEach((plan: Plan) => {
        let planNode: TreeNode = { label: plan.planName, data: plan.planId, expanded: true, children: [], key: plan.planId.toString() };

        let lobNode: TreeNode = { label: '', data: '', children: [] };

        let productNode: TreeNode = { label: '', data: '', children: [] };

        let domainNode: TreeNode = { label: '', data: '', children: [] };

        let measureNode: TreeNode = { label: '', data: '', children: [] };

        plan.lobs.forEach((lob: Lob, index) => {
          lobNode = { label: lob.lobName, data: lob.lobId, expanded: true, children: [], key: plan.planName + lob.lobName + index.toString() };

          lob.products.forEach((product: Product, index) => {
            productNode = { label: product.productName, data: product.productId, children: [], key: plan.planName + lob.lobId + product.productId + index.toString() };

            product.domains.forEach((domain: Domain, index) => {
              domainNode = { label: domain.domainName, data: domain.domainId, children: [], key: plan.planName + lob.lobName + product.productName + domain.domainName + index.toString() };

              domain.measures.forEach((measure: Measure) => {

                measureNode = { label: measure.measureAbbr, data: measure.measureId, key: plan.planName + lob.lobName + product.productName + domain.domainName + measure.measureAbbr + index.toString() };

                domainNode.children.push(measureNode);
              });

              productNode.children.push(domainNode);
            });

            lobNode.children.push(productNode)
          });

          planNode.children.push(lobNode);
        });

        this.projectTreeData.push(planNode);
      });
      this.isProjectTreeLoading = false;
    });
  }

  nodeExpand(node: TreeNode) {
    node.styleClass = this.selectedNodeStyle;
    if (node.parent != undefined && node.parent.expanded && (node.parent.styleClass == undefined || node.parent.styleClass == '')) {
      this.nodeExpand(node.parent);
    }
  }

  nodeCollapse(node: TreeNode) {
    node.expanded = false;
    node.styleClass = '';
    if (node.children) {
      node.children.forEach(childNode => {
        this.nodeCollapse(childNode);
      });
    }
  }

  expandSelectedNode(node: TreeNode) {
    if (node.children && node.children.length > 0 && (node.expanded == false || node.expanded == undefined) && !this.projectTree.hasFilteredNodes()) {
      this.collapseAllNodes(this.projectTreeData);
      this.expandSelectedNodeParentNodes(node)
    }
    else if (node.children && node.children.length > 0 && node.expanded == true && !this.projectTree.hasFilteredNodes()) {
      this.nodeCollapse(node);
    }
    // For Filtered Nodes
    else if (this.projectTree.filteredNodes && this.projectTree.filteredNodes.length > 0) {
      let selectedFilteredNode: TreeNode = this.projectTree.getNodeWithKey(node.key, this.projectTree.filteredNodes);
      if (!selectedFilteredNode.parent) {
        if (selectedFilteredNode.expanded == true) {
          this.nodeCollapse(selectedFilteredNode);
        }
        else {
          this.collapseAllNodes(this.projectTree.filteredNodes);
          this.expandSelectedNodeParentNodes(selectedFilteredNode);
        }
      }
      else {
        let expandedValueOfNode = selectedFilteredNode.expanded;
        this.collapseAllNodes(this.projectTree.filteredNodes);
        if (!selectedFilteredNode.children) {
          this.expandSelectedNodeParentNodes(selectedFilteredNode.parent);
        }
        else {
          if (expandedValueOfNode == true) {
            this.expandSelectedNodeParentNodes(selectedFilteredNode.parent);
          }
          else {
            this.expandSelectedNodeParentNodes(selectedFilteredNode);
          }
        }
      }
    }

  }

  collapseAllNodes(nodes: TreeNode[]) {
    nodes.forEach(node => {
      if (node.children && node.expanded) {
        node.expanded = false;
        node.styleClass = '';
        this.collapseAllNodes(node.children);
      }
    });
  }

  expandSelectedNodeParentNodes(node: TreeNode) {
    node.expanded = true;
    node.styleClass = this.selectedNodeStyle;
    if (node.parent != undefined) {
      this.expandSelectedNodeParentNodes(node.parent);
    }
  }

  onMeasureSelect(node: TreeNode) {
    if (node.children == undefined) {
      this.selectedMeasure = node;
    }
  }

}
